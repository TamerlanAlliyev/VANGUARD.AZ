using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Size : BaseAuditable
{
    public string Name { get; set; } = null!;
    public List<Information>? Information { get; set; } 

}
