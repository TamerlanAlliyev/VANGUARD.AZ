using Newtonsoft.Json;

namespace Vanguard.Areas.Admin.ViewModels.ProductViewModels.Additional;

public class SizeVM
{
    
    public string Size { get; set; } = null!;

    public int Count { get; set; } = default;
    
    public decimal? Weight { get; set; }
    
    public string? Dimensions { get; set; }

}