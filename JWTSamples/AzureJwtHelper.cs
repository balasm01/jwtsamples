using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using RestSharp;

namespace JWTSamples
{
	public static class AzureJwtHelper
	{
		private const string DiscoveryEndPoint =
			"https://login.microsoftonline.com/5b8e5feb-bab9-44e1-a340-9f72cf0bd4ec/discovery/v2.0/keys";

		private const string TokenEndpoint =
			"https://login.microsoftonline.com/5b8e5feb-bab9-44e1-a340-9f72cf0bd4ec/oauth2/v2.0/token";

		private const string OidcConfigEndpoint =
			"https://login.microsoftonline.com/5b8e5feb-bab9-44e1-a340-9f72cf0bd4ec/.well-known/openid-configuration";

		private static AzureKeys _azureKeys;
		private static OpenIdConnectConfiguration _openIdConnectConfiguration;

		static AzureJwtHelper()
		{
			SetAzureKeys();
		}

		public static AzureKeys AzureKeys
		{
			get
			{
				if (_azureKeys == null)
					SetAzureKeys();
				return _azureKeys;
			}
		}
		
		private static void SetAzureKeys()
		{
			var restRequest =
				new RestRequest(DiscoveryEndPoint);
			var restClient = new RestClient();
			_azureKeys = restClient.Execute<AzureKeys>(restRequest).Data;
		}

		public static AzureJwt GetClientCredentialAccessToken(string clientId, string secret, string scope)
		{
			var rc = new RestClient();
			var request = new RestRequest(TokenEndpoint, Method.Post);
			request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
			request.AddBody($"grant_type=client_credentials&client_id={clientId}&client_secret={secret}&scope={scope}");
			var result = rc.ExecutePost<AzureJwt>(request).Data;
			return result;
		}

		public static SecurityToken ValidateToken(AzureJwt token, TokenValidationParameters tokenValidationParameters)
		{
			var azureKey = AzureKeys.Keys.FirstOrDefault(x => x.Kid == token.Header.Kid);
			if (azureKey == null)
				return null;
			var tokenHandler = new JwtSecurityTokenHandler();
			tokenHandler.ValidateToken(token.AccessToken, tokenValidationParameters, out var securityToken);
			return securityToken;

		}

		public static OpenIdConnectConfiguration GetOpenIdConnectConfiguration()
		{
			return _openIdConnectConfiguration ??= OpenIdConnectConfigurationRetriever
				.GetAsync(
					OidcConfigEndpoint, new CancellationToken()).GetAwaiter().GetResult();
		}

		public static TokenValidationParameters GetTokenValidationParameters(string issuer, string audience)
		{
			return new TokenValidationParameters()
			{
				ValidAudience = audience,
				ValidIssuer = issuer,
				IssuerSigningKeys = GetOpenIdConnectConfiguration().SigningKeys
			};
		}

	}
}