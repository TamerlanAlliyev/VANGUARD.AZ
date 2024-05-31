using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models
{
    public class Gender:BaseAuditable
    {
        public string Name { get; set; } = null!;
        public List<Product>? Products { get; set; }
    }
}
