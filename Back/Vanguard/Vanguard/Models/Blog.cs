using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Blog:BaseAuditable
{
    public int Clickeds { get; set; }
    public string Title { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
    public AppUser AppUser { get; set; } = null!;
    [AllowHtml]
    public string MainDescription { get; set; } = null!;
    [AllowHtml]
    public string? AddinationDescription { get; set; }

    public List<BlogCategory> Categories { get; set; } = null!;
    public ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();
    public string? MainPicture { get; set; }
    public string? AddinationPicture { get; set; }
    [NotMapped]
    public IFormFile? MainFile { get; set; }
    [NotMapped]
    public IFormFile? AddinationFile { get; set; }

    public List<Image> Images { get; set; } = null!;

}
