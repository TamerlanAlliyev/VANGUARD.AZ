using Vanguard.Models;

namespace Vanguard.ViewModels.Contact;

public class ContactVM
{
    public Vanguard.Models.Contact Contact { get; set; } = new Vanguard.Models.Contact();

    public Connection? CustomerConnection { get; set; } 
}
