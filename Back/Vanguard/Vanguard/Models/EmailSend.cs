using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Vanguard.Models.BaseEntitys;

namespace Vanguard.Models;

public class EmailSend:BaseAuditable
{
	public string SendingMethod { get; set; }
	[DataType(DataType.EmailAddress)]
	public string? Email { get; set; }
	public string Subject { get; set; } = null!;
	public string Body { get; set; } = null!;
	public string? FileUrl { get; set; }
	public string? FileType { get; set; }
}
