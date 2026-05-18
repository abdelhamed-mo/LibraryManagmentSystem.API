namespace ServiceAbstraction
{
	public interface IAuthenticationService
	{
		Task<UserResultDto> LoginAsync(LoginDto login);
		Task<UserResultDto> RegisterAsync(RegisterDto register);
		Task<string> ConfirmEmailAsync(string email, string token);
		Task<string> ForgotPasswordAsync(string email);
		Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
	}
}
