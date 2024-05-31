using System.ComponentModel.DataAnnotations.Schema;

namespace Vanguard.Models;

public class Wish
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int SizeId { get; set; }
    public Size Size { get; set; } = null!;
    public Product Product { get; set; } = null!;
    public string UserId { get; set; } = null!;
    [ForeignKey("UserId")]
    public AppUser User { get; set; } = null!;
}
