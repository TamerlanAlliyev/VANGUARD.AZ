using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vanguard.Models;

public class AllowedEmployee
{
	public int Id { get; set; }
	public string RoleId { get; set; } = null!;

	[Required]
	[EmailAddress]
	public string Email { get; set; } = null!;

	[Required]
	public string AccessCode { get; set; } = null!;

	public string? AppUserId { get; set; }

	[ForeignKey("AppUserId")]
	public AppUser? AppUser { get; set; }
	public IdentityRole Role { get; set; } = null!;
}
