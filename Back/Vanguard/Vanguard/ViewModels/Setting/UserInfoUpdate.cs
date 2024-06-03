using System.ComponentModel.DataAnnotations;

namespace Vanguard.ViewModels.Setting;

public class UserInfoUpdate
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string SurName { get; set; } = null!;
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Postal { get; set; }
    public string? HomeAddress { get; set; }

}
