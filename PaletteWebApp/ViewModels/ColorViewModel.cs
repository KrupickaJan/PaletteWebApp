namespace PaletteWebApp.ViewModels;

public class ColorViewModel
{
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
    public string HexCode => $"#{R:X2}{G:X2}{B:X2}";
}
