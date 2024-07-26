namespace PaletteGeneratorLibrary;
internal class ColorCluster
{
	private int _rDimension;
	private int _gDimension;
	private int _bDimension;
	private int _maxDimension;
	private int _volume;
	private List<ColorPoint> _pixels;

	public ColorCluster(List<ColorPoint> pixels)
	{
		_pixels = pixels;
		CountDimensionsOfColorBox();
		CountMaxDimensionsOfColorBox();
		CountVolumeOfColorBox();
	}

	public int RDimension { get => _rDimension; }
	public int GDimension { get => _gDimension; }
	public int BDimension { get => _bDimension; }
	public int MaxDimension { get => _maxDimension; }
	public ColorPoint AverageColor { get => ColorUtil.CountAverageColor(_pixels); }
	public int Volume { get => _volume; }
	public List<ColorPoint> Pixels
	{
		get => _pixels;
		set
		{
			_pixels = value;
			CountDimensionsOfColorBox();
			CountMaxDimensionsOfColorBox();
			CountVolumeOfColorBox();
		}
	}
	private void CountVolumeOfColorBox()
	{
		_volume = _rDimension * _gDimension * _bDimension;
	}
	private void CountDimensionsOfColorBox()
	{
		int rMin = _pixels.OrderBy(colorPoint => colorPoint.Color.Red).FirstOrDefault()?.Color.Red ?? 0;
		int rMax = _pixels.OrderByDescending(colorPoint => colorPoint.Color.Red).FirstOrDefault()?.Color.Red ?? 0;
		int gMin = _pixels.OrderBy(colorPoint => colorPoint.Color.Green).FirstOrDefault()?.Color.Green ?? 0;
		int gMax = _pixels.OrderByDescending(colorPoint => colorPoint.Color.Green).FirstOrDefault()?.Color.Green ?? 0;
		int bMin = _pixels.OrderBy(colorPoint => colorPoint.Color.Blue).FirstOrDefault()?.Color.Blue ?? 0;
		int bMax = _pixels.OrderByDescending(colorPoint => colorPoint.Color.Blue).FirstOrDefault()?.Color.Blue ?? 0;

		_rDimension = rMax - rMin;
		_gDimension = gMax - gMin;
		_bDimension = bMax - bMin;
	}
	private void CountMaxDimensionsOfColorBox()
	{
		_maxDimension = Math.Max(_rDimension, Math.Max(_gDimension, _bDimension));
	}
}
