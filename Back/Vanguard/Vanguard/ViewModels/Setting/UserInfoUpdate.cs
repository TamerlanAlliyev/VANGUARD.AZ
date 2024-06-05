using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vanguard.ViewModels.Setting;

public class UserInfoUpdate
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string SurName { get; set; } = null!;
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    public string? Image { get; set; }
    public string? Phone { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Postal { get; set; }
    public string? HomeAddress { get; set; }

    [NotMapped]
    public IFormFile? ProfilImage {  get; set; }

}
