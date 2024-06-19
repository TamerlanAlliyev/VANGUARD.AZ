using System.ComponentModel.DataAnnotations.Schema;
using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class HomeBanner 
{
    public int Id { get; set; }
	public string Title { get; set; } = null!;
	public string? SubTitle { get; set; }
	public int? CategoryId { get; set; }
	public Category? Category { get; set; }

    public int? ImageId { get; set; }
    public Image Image { get; set; } = null!;

    [NotMapped]
    public IFormFile ImageFile { get; set; } = null!;

}
