using Newtonsoft.Json;

namespace Vanguard.Areas.Admin.ViewModels.ProductViewModels.Edit;

public class SizeVM
{
    public int? Id { get; set; }
    public string? Size { get; set; }

    public int Count { get; set; } = default;
    
    public decimal? Weight { get; set; }
    
    public string? Dimensions { get; set; }

}