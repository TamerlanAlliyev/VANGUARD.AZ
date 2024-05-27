using System.ComponentModel.DataAnnotations.Schema;

namespace Vanguard.Areas.Admin.ViewModels.ProductViewModels.Additional;

public class ImageVM
{
    public string MainImageURL { get; set; } = null!;
    public string HoverImageURL { get; set; } = null!;
    public List<string>? AdditionalImagesURL { get; set; }

    [NotMapped]
    public IFormFile MainImage { get; set; } = null!;
    [NotMapped]
    public IFormFile HoverImage { get; set; } = null!;
    [NotMapped]
    public List<IFormFile>? AdditionalImages { get; set; }

}
