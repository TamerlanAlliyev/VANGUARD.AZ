using System.ComponentModel.DataAnnotations;
using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Connection:BaseAuditable
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Message { get; set; } = null!;
    public string? SendEmal { get; set; } 
    public bool IsRead { get; set; }
    public bool IsSend { get; set; }
}
