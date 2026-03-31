using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.AuthenticationModels;

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
	}
}
