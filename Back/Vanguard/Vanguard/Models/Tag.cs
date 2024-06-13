using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class Tag:BaseAuditable
{
    public string Name { get; set; } = null!;
    public List<ProductTag> ProductTag { get; set; } = new List<ProductTag>();
    public List<BlogTag> BlogTags { get; set; } = new List<BlogTag>();

    public int? HomeSliderId { get; set; }
    public List<HomeSlider>? HomeSliders { get; set; }
}
