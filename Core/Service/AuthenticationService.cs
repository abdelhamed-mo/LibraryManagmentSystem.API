using Microsoft.AspNetCore.WebUtilities;

namespace Service
{
	public class AuthenticationService(UserManager<User> userManager, IConfiguration configuration, IEmailService emailService) : IAuthenticationService
	{
		public async Task<UserResultDto> LoginAsync(LoginDto login)
		{
			var user = await userManager.FindByEmailAsync(login.Email);
			if (user == null) throw new UnAuthorizedException("Email Doesn't Exist");

			var result = await userManager.CheckPasswordAsync(user, login.Password);
			if (!result) throw new UnAuthorizedException("Wrong Password");

			if (!user.EmailConfirmed) throw new UnAuthorizedException("User Not Confirmed! \n Please Check Your Email");

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

			var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
			var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
			var confirmationLink = $"https://localhost:7271/api/Authentication/ConfirmEmail?email={user.Email}&token={encodedToken}";
			emailService.EmailSender(user.Email, "Email Confirmation", $"Click the link to confirm your email: {confirmationLink}");

			return new UserResultDto()
			{
				Email = user.Email,
				DisplayName = user.DisplayName,
				UserName = user.UserName,
				Token = await GenerateTokenAsync(user),
			};
		}
		public async Task<string> ConfirmEmailAsync(string email, string token)
		{
			var user = await userManager.FindByEmailAsync(email);

			if (user == null) throw new UnAuthorizedException("Email Doesn't Exist");

			if (user.EmailConfirmed) return "Email Confirmed Before!";

			var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

			var result = await userManager.ConfirmEmailAsync(user, decodedToken);

			if (!result.Succeeded) throw new ValidationException(result.Errors.Select(e => e.Description));

			return "Email Confirmed Successfully";
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
		public async Task<string> ForgotPasswordAsync(string email)
		{
			var user = await userManager.FindByEmailAsync(email);
			if (user == null) throw new UnAuthorizedException("Email Doesn't Exist");
			var token = await userManager.GeneratePasswordResetTokenAsync(user);
			var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
			var resetLink = $"https://localhost:7271/api/Authentication/ResetPassword?email={email}&token={encodedToken}";
			emailService.EmailSender(email, "Password Reset", $"Click the link to reset your password: {resetLink}");
			return "Reset link sent successfully";
		}
		public async Task<string> ResetPasswordAsync(ResetPasswordDto resetPassword)
		{
			var user = await userManager.FindByEmailAsync(resetPassword.Email);
			if (user == null) throw new UnAuthorizedException("Email Doesn't Exist");
			var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPassword.Token));
			var result = await userManager.ResetPasswordAsync(user, decodedToken, resetPassword.NewPassword);
			if (!result.Succeeded) throw new ValidationException(result.Errors.Select(e => e.Description));
			return "Password Changed Successfully";
		}

	}
}
