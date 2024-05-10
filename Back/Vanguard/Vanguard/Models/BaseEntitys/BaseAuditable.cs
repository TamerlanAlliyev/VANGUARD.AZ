namespace Vanguard.Models.BaseEntitys;

public class BaseAuditable:BaseEntity
{
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set;}
    public string IPAddress { get; set; } = null!;
}
