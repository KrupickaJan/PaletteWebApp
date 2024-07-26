using SkiaSharp;

namespace PaletteGeneratorLibrary;
internal class ColorUtil
{
	public static ColorPoint CountAverageColor(List<ColorPoint> colors)
	{
		byte rAverage = (byte)Math.Round(colors.Average(colorPoint => colorPoint.Color.Red));
        byte gAverage = (byte)Math.Round(colors.Average(colorPoint => colorPoint.Color.Green));
        byte bAverage = (byte)Math.Round(colors.Average(colorPoint => colorPoint.Color.Blue));

		return new ColorPoint(new SKColor(rAverage, gAverage, bAverage));
	}
}
