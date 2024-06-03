using Microsoft.AspNetCore.Http.HttpResults;

namespace Vanguard.Areas.Admin.ViewModels.Admin;

public class EmployeeListVM
{
    public int Id { get; set; }
    public string? Image { get; set; } 
    public string? FullName { get; set; }
    public string Position { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public bool IsRegister { get; set; }
}
