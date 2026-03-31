namespace ServiceAbstraction
{
	public interface IAuthenticationService
	{
		Task<UserResultDto> LoginAsync(LoginDto login);
		Task<UserResultDto> RegisterAsync(RegisterDto register);
	}
}
