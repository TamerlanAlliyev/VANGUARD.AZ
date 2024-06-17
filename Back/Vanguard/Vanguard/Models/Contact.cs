using System.ComponentModel.DataAnnotations;

namespace Vanguard.Models;

public class Contact
{
    public int Id { get; set; }
    public string Address { get; set; } = "Port Baku Mall, 151 Neftchilar Avenue, Baku";
    public string AddressLink { get; set; } = "https://maps.app.goo.gl/WWYfJmJJxduwx3Ne9";
    public string AddressUrl { get; set; } = null!;
	public string Number { get; set; } = "+994507101015";
	[DataType(DataType.EmailAddress)]
    public string Email { get; set; } = "vanguardfashionaz@gmail.com";
}
