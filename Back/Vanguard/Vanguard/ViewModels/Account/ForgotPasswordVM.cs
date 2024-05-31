using System.ComponentModel.DataAnnotations;

namespace Vanguard.ViewModels.Account;

public class ForgotPasswordVM
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
}
