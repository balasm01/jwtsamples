using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace JWTSamples
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var input =
				@"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6IjJaUXBKM1VwYmpBWVhZR2FYRUpsOGxWMFRPSSIsImtpZCI6IjJaUXBKM1VwYmpBWVhZR2FYRUpsOGxWMFRPSSJ9";
			Console.WriteLine(input);
			var decodeFromBase64String = EncodingExtensions.DecodeFromBase64String(input);
			Console.WriteLine(decodeFromBase64String);
			Console.WriteLine(EncodingExtensions.EncodeToBase64String(decodeFromBase64String));
			var token = AzureJwtHelper.GetClientCredentialAccessToken("082ab154-8415-4f2a-b83b-e2dfc0b14991",
				"9sT8Q~NIr36-MYN6dWXO_wXToruRld-EWJhP.boz", "api://082ab154-8415-4f2a-b83b-e2dfc0b14991/.default");
			var azKeys = AzureJwtHelper.AzureKeys;

			Console.WriteLine(azKeys);
			var tvp = AzureJwtHelper.GetTokenValidationParameters(
				"https://sts.windows.net/5b8e5feb-bab9-44e1-a340-9f72cf0bd4ec/",
				"api://082ab154-8415-4f2a-b83b-e2dfc0b14991");
			var securityToken = AzureJwtHelper.ValidateToken(token, tvp);
			Console.WriteLine(securityToken.ValidFrom + " " + securityToken.ValidTo);
			var user = new User
				{ Username = "balasm01", Created = DateTime.Now, Expiry = DateTime.Now.AddMinutes(20) };
			user = SecurityExtensions.PopulateJwtSecurityToken(user);

			Console.WriteLine(user.SecurityToken.AsJsonString());
			Console.WriteLine(user.Token);
			Console.WriteLine(SecurityExtensions.ValidateUserToken(user.Token));
			Console.Read();
		}
	}
}