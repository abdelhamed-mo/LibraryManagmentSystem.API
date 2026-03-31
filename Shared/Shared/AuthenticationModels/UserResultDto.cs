namespace Shared.AuthenticationModels
{
	public record UserResultDto
	{
		public string UserName { get; set; }
		public string DisplayName { get; set; }
		public string Email { get; set; }
		public string Token { get; set; }
	}
}
