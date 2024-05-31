using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Vanguard.Models;

namespace Vanguard.Areas.Admin.ViewModels.Admin
{
    public class EmployeeCreatVM
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        public string RoleId { get; set; }
        public List<IdentityRole>? Roles { get; set; }
    }
}
