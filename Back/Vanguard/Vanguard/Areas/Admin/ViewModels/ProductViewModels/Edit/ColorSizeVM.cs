namespace Vanguard.Areas.Admin.ViewModels.ProductViewModels.Edit;

public class ColorSizeVM
{
    public string ColorName { get; set; } = null!;
    public string HexCode { get; set; } = null!;
    public List<SizeVM> Sizes { get; set; } = new List<SizeVM>();
    public ImageVM Images { get; set; } = null!;
}