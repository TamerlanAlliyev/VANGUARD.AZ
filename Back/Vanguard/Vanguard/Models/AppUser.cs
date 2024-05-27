using Microsoft.AspNetCore.Identity;

namespace Vanguard.Models;

public class AppUser:IdentityUser
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string FullName { get; set; } = null!;
}
