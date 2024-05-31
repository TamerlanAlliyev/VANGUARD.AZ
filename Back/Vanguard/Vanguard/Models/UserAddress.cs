namespace Vanguard.Models;

public class UserAddress
{
    public int Id { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? HomeAddress { get; set; }
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;
}
