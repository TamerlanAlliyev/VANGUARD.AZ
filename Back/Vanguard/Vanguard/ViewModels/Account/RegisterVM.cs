using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vanguard.ViewModels.Account;

public class RegisterVM
{
    public string Name { get; set; } = null!;
    public string Surname{ get; set; } = null!;
    public string? RoleId { get; set; } 

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [DataType(DataType.Password), Compare(nameof(Password))]
    public string ConfirmPassword { get; set; } = null!;

    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }
    public string? ProfilImage { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
