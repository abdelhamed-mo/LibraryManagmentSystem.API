namespace Service
{
	public class AuthenticationService(UserManager<User> userManager, IConfiguration configuration) : IAuthenticationService
	{
		public async Task<UserResultDto> LoginAsync(LoginDto login)
		{
			var user = await userManager.FindByEmailAsync(login.Email);
			if (user == null) throw new UnAuthorizedException("Email Doesn't Exist");

			var result = await userManager.CheckPasswordAsync(user, login.Password);
			if (!result) throw new UnAuthorizedException("Wrong Password");

			return new UserResultDto()
			{
				Email = user.Email,
				DisplayName = user.DisplayName,
				UserName = user.UserName,
				Token = await GenerateTokenAsync(user),
			};
		}
		public async Task<UserResultDto> RegisterAsync(RegisterDto register)
		{
			var user = new User()
			{
				Email = register.Email,
				DisplayName = register.DisplayName,
				UserName = register.UserName,
				PhoneNumber = register.PhoneNumber,
			};
			var result = await userManager.CreateAsync(user, register.Password);

			if (!result.Succeeded) throw new ValidationException(result.Errors.Select(e => e.Description));

			return new UserResultDto()
			{
				Email = user.Email,
				DisplayName = user.DisplayName,
				UserName = user.UserName,
				Token = await GenerateTokenAsync(user),
			};
		}
		private async Task<string> GenerateTokenAsync(User user)
		{
			var issuer = configuration.GetSection("JwtOptions")["Issuer"];
			var expire = DateTime.UtcNow.AddDays(double.Parse(configuration.GetSection("JwtOptions")["DurationInDays"]));
			var audience = configuration.GetSection("JwtOptions")["Audience"];
			var secretKey = configuration.GetSection("JwtOptions")["SecretKey"];
			var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
			var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
			var roles = await userManager.GetRolesAsync(user);
			//var role = roles.FirstOrDefault();
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id),
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Email, user.Email),
			};
			foreach (var role in roles)
				claims.Add(new Claim(ClaimTypes.Role, role));

			var token = new JwtSecurityToken(
					signingCredentials: signingCredentials,
					issuer: issuer,
					audience: audience,
					expires: expire,
					claims: claims);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
