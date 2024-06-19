using Vanguard.Models;

namespace Vanguard.ViewModels.About;

public class AboutVM
{
    public Vanguard.Models.About About { get; set; } = null!;
    public List<AboutAccordion> AboutAccordions { get; set; } = new List<AboutAccordion>();
    public List<AboutEmploees> Emploees { get; set; } =  new List<AboutEmploees>();
    public Vanguard.Models.Contact Contact { get; set; }=new Vanguard.Models.Contact();
}
