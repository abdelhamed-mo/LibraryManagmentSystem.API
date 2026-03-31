using System.ComponentModel.DataAnnotations;

namespace Shared.AuthenticationModels
{
	public record RegisterDto
	{
		[EmailAddress]
		[Required(ErrorMessage = "Email is required")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Password is required")]
		public string Password { get; set; }
		[Required(ErrorMessage = "UserName is required")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "DisplayName is required")]
		public string DisplayName { get; set; }
		[Required(ErrorMessage = "PhoneNumber is required")]
		public string PhoneNumber { get; set; }
	}
}
