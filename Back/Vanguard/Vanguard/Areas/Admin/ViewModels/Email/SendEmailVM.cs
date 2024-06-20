using Vanguard.Models;

namespace Vanguard.Areas.Admin.ViewModels.Email;

public class SendEmailVM
{
    public Connection Message { get; set; } = null!;
    public string Email { get; set; } = null!;

}
