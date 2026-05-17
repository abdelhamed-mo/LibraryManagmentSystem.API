using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthenticationModels
{
	public record ResetPasswordDto
	{
		public string Email { get; set; }
		public string Token { get; set; }
		public string NewPassword { get; set; }
	}
}
