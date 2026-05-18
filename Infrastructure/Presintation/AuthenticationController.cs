namespace Presentation
{
	public class AuthenticationController(IServiceManager serviceManager) : ApiBaseController
	{
		[HttpPost("Login")]
		public async Task<ActionResult<UserResultDto>> Login([FromBody] LoginDto loginDto)
			=> Ok(await serviceManager.AuthenticationService.LoginAsync(loginDto));
		[HttpPost("Register")]
		public async Task<ActionResult<UserResultDto>> Register([FromBody] RegisterDto registerDto)
			=> Ok(await serviceManager.AuthenticationService.RegisterAsync(registerDto));
		[HttpPost("ConfirmEmail")]
		public async Task<ActionResult> ConfirmEmail(string email, string token)
			=> Ok(await serviceManager.AuthenticationService.ConfirmEmailAsync(email, token));
		[HttpPost("ForgotPassword")]
		public async Task<ActionResult> ForgotPassword(string email)
			=> Ok(await serviceManager.AuthenticationService.ForgotPasswordAsync(email));
		[HttpPost("ResetPassword")]
		public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
			=> Ok(await serviceManager.AuthenticationService.ResetPasswordAsync(resetPasswordDto));
	}
}
