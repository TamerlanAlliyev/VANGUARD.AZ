
using System.ComponentModel.DataAnnotations.Schema;

namespace Vanguard.Areas.Admin.ViewModels.ProductViewModels.Edit;

public class ImageVM
{
    public string? MainImageURL { get; set; }
    public string? HoverImageURL { get; set; } 
    public List<string>? AdditionalImagesURL { get; set; }

    [NotMapped]
    public IFormFile? MainImage { get; set; } 
    [NotMapped]
    public IFormFile? HoverImage { get; set; } 
    [NotMapped]
    public List<IFormFile>? AdditionalImages { get; set; }

}
