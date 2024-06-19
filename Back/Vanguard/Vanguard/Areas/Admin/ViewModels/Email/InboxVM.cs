using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Vanguard.Areas.Admin.ViewModels.Email;

public class InboxVM
{
    public Connection Message { get; set; } = null!;

}
