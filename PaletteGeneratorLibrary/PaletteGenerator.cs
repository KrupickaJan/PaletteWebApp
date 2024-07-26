using SkiaSharp;

namespace PaletteGeneratorLibrary;
public class PaletteGenerator
{
    private const int MAXIMUM_BITMAP_SIZE = 1000000;

    private int _rawPaletteDimansion;
    private SKBitmap _bitmap;
    private int _paletteDimension;
    private List<ColorPoint> _palette;
    private List<ColorPoint> _pixels;
    private List<ColorCluster> _colorCLusters;

    public PaletteGenerator(SKBitmap bitmap, int paletteDimension)
    {
        if (paletteDimension > 0)
        {
            _paletteDimension = paletteDimension;
        }
        else
        {
            _paletteDimension = 1;
        }
        _bitmap = bitmap;
        _palette = new();
        _pixels = new();
        _colorCLusters = new();
        ExtractPalette();
    }

    public List<ColorPoint> Palette { get => _palette; }

    private void ExtractPalette()
    {
        SetPaletteDimansion();
        ResizeBitmap();
        ExtractPixels();
        CreateClusters();
        CreateRawPalette();
        FilterRawPalette();
        SortPalette();
    }
    private void SetPaletteDimansion()
    {
        if (_paletteDimension > 1)
        {
            _rawPaletteDimansion = _paletteDimension * 6;
        }
    }
    private void ResizeBitmap()
    {
        if (_bitmap.Width * _bitmap.Height > MAXIMUM_BITMAP_SIZE)
        {
            int width = (int)Math.Sqrt(((double)MAXIMUM_BITMAP_SIZE * (double)_bitmap.Width) / (double)_bitmap.Height);
            int height = MAXIMUM_BITMAP_SIZE / width;
            SKBitmap newBitmap = new SKBitmap(width, height);
            _bitmap.ScalePixels(newBitmap, SKFilterQuality.High);
            _bitmap = newBitmap;
        }
    }
    private void ExtractPixels()
    {
        for (int i = 0; i < _bitmap.Width; i++)
        {
            for (int j = 0; j < _bitmap.Height; j++)
            {
                _pixels.Add(new ColorPoint(_bitmap.GetPixel(i, j)));
            }
        }
    }
    private void CreateClusters()
    {
        _colorCLusters = new() { new ColorCluster(_pixels) };
    }
    private void CreateRawPalette()
    {
        ResizeBitmap();
        for (int i = 1; i < _rawPaletteDimansion; i++)
        {
            ColorCluster clusterToSplit = GetBoxToSplit();
            _colorCLusters.Remove(clusterToSplit);
            _colorCLusters.AddRange(SplitColorCluster(clusterToSplit));
        }
        _colorCLusters.ForEach(cluster => { _palette.Add(cluster.AverageColor); });
    }

    private void FilterRawPalette()
    {

        ColorPoint[] colorsToJoin = new ColorPoint[2];
        double minDistance = double.MaxValue;

        while (_paletteDimension != _palette.Count)
        {
            for (int i = 0; i < _palette.Count; i++)
            {
                for (int j = i + 1; j < _palette.Count; j++)
                {
                    double distance = _palette[i].GetRgbDistance(_palette[j]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        colorsToJoin[0] = _palette[i];
                        colorsToJoin[1] = _palette[j];
                    }
                }
            }
            _palette.Remove(colorsToJoin[0]);
            _palette.Remove(colorsToJoin[1]);
            _palette.Add(ColorUtil.CountAverageColor(new List<ColorPoint> { colorsToJoin[0], colorsToJoin[1] }));
            minDistance = double.MaxValue;
        }
    }

    //private void SortPalette()
    //{
    //    List<int> visited = new List<int> { 0 };
    //    bool[] visitedFlags = new bool[_palette.Count];
    //    visitedFlags[0] = true;

    //    for (int j = 1; j < _palette.Count; j++)
    //    {
    //        int lastVisited = visited.Last();
    //        int nextColorIndex = -1;
    //        double minDistance = double.MaxValue;

    //        for (int i = 0; i < _palette.Count; i++)
    //        {
    //            if (!visitedFlags[i])
    //            {
    //                double distance = _palette[lastVisited].GetRgbDistance(_palette[i]);
    //                if (distance < minDistance)
    //                {
    //                    minDistance = distance;
    //                    nextColorIndex = i;
    //                }
    //            }
    //        }

    //        if (nextColorIndex != -1)
    //        {
    //            visited.Add(nextColorIndex);
    //            visitedFlags[nextColorIndex] = true;
    //        }
    //    }

    //    List<ColorPoint> sortedColors = visited.Select(index => _palette[index]).ToList();
    //    _palette = sortedColors;
    //}

    private void SortPalette()
    {
        _palette = _palette.OrderBy(p => p.Lightnes).ToList();
    }

    private ColorCluster[] SplitColorCluster(ColorCluster box)
    {
        if (box.Pixels.Distinct().Count() == 1)
        {
            ColorCluster defaulCluster = new ColorCluster(new List<ColorPoint> { box.Pixels.FirstOrDefault() ?? new() });
            return [defaulCluster, defaulCluster];
        }
        else if (box.MaxDimension == box.RDimension)
        {
            return SplitClusterByChanel(box, SelectRedChanelOfColorPoint);
        }
        else if (box.MaxDimension == box.GDimension)
        {
            return SplitClusterByChanel(box, SelectGreenChanelOfColorPoint);
        }
        else
        {
            return SplitClusterByChanel(box, SelectBlueChanelOfColorPoint);
        }
    }

    private void DistributeCuttingColorPoints(ColorCluster colorBoxA, ColorCluster colorBoxB, List<ColorPoint> cuttingColorPoints)
    {
        if (colorBoxA.Pixels.Count < colorBoxB.Pixels.Count)
        {
            colorBoxA.Pixels.AddRange(cuttingColorPoints);
        }
        else
        {
            colorBoxB.Pixels.AddRange(cuttingColorPoints);
        }
    }

    private ColorCluster GetBoxToSplit()
    {
        return _colorCLusters.OrderByDescending(box => box.Volume).First();
    }

    private ColorCluster[] SplitClusterByChanel(ColorCluster box, Func<ColorPoint, int> selectChanel)
    {
        List<ColorPoint> orderedList = box.Pixels.OrderBy(colorPoint => selectChanel(colorPoint)).ToList();
        int cuttingValue = selectChanel(orderedList[orderedList.Count / 2]);
        ColorCluster colorBoxA = new(orderedList.Where(colorPoint => selectChanel(colorPoint) < cuttingValue).ToList());
        ColorCluster colorBoxB = new(orderedList.Where(colorPoint => selectChanel(colorPoint) > cuttingValue).ToList());
        List<ColorPoint> cuttingColorPoints = orderedList.Where(colorPoint => selectChanel(colorPoint) == cuttingValue).ToList();

        DistributeCuttingColorPoints(colorBoxA, colorBoxB, cuttingColorPoints);

        return [colorBoxA, colorBoxB];
    }

    private int SelectRedChanelOfColorPoint(ColorPoint colorPoint)
    {
        return colorPoint.Color.Red;
    }

    private int SelectGreenChanelOfColorPoint(ColorPoint colorPoint)
    {
        return colorPoint.Color.Green;
    }

    private int SelectBlueChanelOfColorPoint(ColorPoint colorPoint)
    {
        return colorPoint.Color.Blue;
    }
}
