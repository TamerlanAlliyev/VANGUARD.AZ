using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vanguard.Models;

public class AppUser : IdentityUser
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public int? UserAddressId { get; set; }
    public UserAddress? UserAddress { get; set; }
    public string? ProfilImage { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }
    public int? ImageId { get; set; }
    public Image? Image { get; set; }

    public int? AllowedEmployeeId { get; set; } 
    public AllowedEmployee? AllowedEmployee { get; set; }
}
