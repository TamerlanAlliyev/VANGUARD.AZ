using System.ComponentModel.DataAnnotations;

namespace Vanguard.Models;

public class Subscription
{
    [Key]
    public int Id { get; set; }
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
}
