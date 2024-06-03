using System.ComponentModel.DataAnnotations.Schema;

namespace Vanguard.Models;

public class Basket
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int InformationId { get; set; }
    public Information Information { get; set; } = null!;
    public string AppUserId { get; set; } = null!;
    [ForeignKey("AppUserId")]
    public AppUser AppUser { get; set; } = null!;
}
