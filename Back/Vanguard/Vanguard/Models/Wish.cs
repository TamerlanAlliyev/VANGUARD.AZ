using System.ComponentModel.DataAnnotations.Schema;

namespace Vanguard.Models;

public class Wish
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
    [ForeignKey("AppUserId")]
    public AppUser AppUser { get; set; } = null!;
}
