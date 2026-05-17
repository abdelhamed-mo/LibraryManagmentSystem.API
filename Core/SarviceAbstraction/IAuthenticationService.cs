namespace ServiceAbstraction
{
	public interface IAuthenticationService
	{
		Task<UserResultDto> LoginAsync(LoginDto login);
		Task<UserResultDto> RegisterAsync(RegisterDto register);
		Task<string> ForgotPasswordAsync(string email);
		Task<string> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
	}
}
