namespace Vanguard.Models;

public class SettingHomeHero
{
    public int Id { get; set; }
    public int? Offer { get; set; }
    public string HeroName { get; set; } = "Default Hero Name";
    public string Title { get; set; } = "Default Title";
    public string Description { get; set; } = "Default Description";
    public DateTime? Time{ get; set; } =new DateTime();
    public int? CategoryId { get; set; }
    public int? TagId { get; set; }
    public Category? Category { get; set; }
    public Tag? Tag { get; set; }
}
