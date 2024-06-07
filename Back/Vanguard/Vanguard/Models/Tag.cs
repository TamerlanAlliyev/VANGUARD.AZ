using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Tag:BaseAuditable
{
    public string Name { get; set; } = null!;
    public List<ProductTag> ProductTag { get; set; }=null!;
    public List<BlogTag> Tags { get; set; } = null!;

    public int? HomeSliderId { get; set; }
    public List<HomeSlider>? HomeSliders { get; set; }

}
