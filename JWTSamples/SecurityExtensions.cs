using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JWTSamples
{
	public static class SecurityExtensions
	{
		public static User PopulateJwtSecurityToken(User user)
		{
			var signingKey = Encoding.UTF8.GetBytes("MyLongJwtSecretKey");
			var signingCredentials =
				new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.RsaSha256);

			var claim = new Claim(ClaimTypes.Name, user.Username);
			var claims = new[] { claim };
			var claimsIdentity = new ClaimsIdentity(claims);
			var securityTokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = claimsIdentity, Expires = user.Expiry, SigningCredentials = signingCredentials,
				Issuer = "JWTSamples", Audience = "JWTSamples"
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			user.SecurityToken = tokenHandler.CreateToken(securityTokenDescriptor);

			user.Token = tokenHandler.WriteToken(user.SecurityToken);

			return user;
		}

		public static SecurityToken ValidateUserToken(string token)
		{
			var signingKey = Encoding.UTF8.GetBytes("MyLongJwtSecretKey");
			var securityKey = new SymmetricSecurityKey(signingKey);

			var tokenValidationParams = new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				IssuerSigningKey = securityKey,
				ValidateLifetime = false
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var result = tokenHandler.ValidateToken(token, tokenValidationParams, out var validatedSecurityToken);
			Console.WriteLine(tokenHandler.WriteToken(validatedSecurityToken));
			Console.WriteLine(validatedSecurityToken.ValidFrom);
			Console.WriteLine(validatedSecurityToken.ValidTo);
			return validatedSecurityToken;
		}
	}
}