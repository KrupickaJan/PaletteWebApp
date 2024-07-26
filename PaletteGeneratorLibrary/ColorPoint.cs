using SkiaSharp;

namespace PaletteGeneratorLibrary;
public class ColorPoint
{
	private SKColor _color;
	private readonly float _hue;
    private readonly float _saturation;
    private readonly float _lightnes;
    public ColorPoint(SKColor color)
	{
		_color = color;
		_color.ToHsl(out float hue, out float saturation, out float luminosity);
		_hue = hue;
		_saturation = saturation;
		_lightnes = luminosity;
	}

	public ColorPoint()
	{
		_color = SKColor.Empty;
    }

	public SKColor Color { get => _color; set => _color = value;  }
	public double Lightnes { get => _lightnes; }
	public double Hue { get => _hue; }
	public double Saturation { get => _saturation; }
	public double GetRgbDistance(ColorPoint colorB)
	{
		return Math.Sqrt
			(
			Math.Pow(_color.Red - colorB.Color.Red, 2) + 
			Math.Pow(_color.Green - colorB.Color.Green, 2) + 
			Math.Pow(_color.Blue - colorB.Color.Blue, 2)
			);
	}
	public double GetHslDistance(ColorPoint colorB)
	{
        return Math.Sqrt
            (
            Math.Pow(_hue - colorB.Hue, 2) +
            Math.Pow(_saturation - colorB.Saturation, 2) +
            Math.Pow(_lightnes - colorB.Lightnes, 2)
            );
    }
}
