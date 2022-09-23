using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace JWTSamples
{
	public class AzureJwt
	{
		[JsonPropertyName("token_type")]
		public string TokenType { get; set; }
		[JsonPropertyName("expires_in")]
		public int Expires { get; set; }
		[JsonPropertyName("ext_expires_in")]
		public int ExtendedExpiresIn { get; set; }
		[JsonPropertyName("access_token")]
		public string AccessToken { get; set; }

		public AzureJwtHeader Header => JsonConvert.DeserializeObject<AzureJwtHeader>(EncodingExtensions.DecodeFromBase64String(AccessToken.Split('.')[0]));
		public string Payload => EncodingExtensions.DecodeFromBase64String(AccessToken.Split('.')[1]);
		public string Signature => AccessToken.Split('.')[2];

		public byte[] TokenHash
		{
			get
			{
				var sha = SHA256.Create();
				return sha.ComputeHash(
					Encoding.UTF8.GetBytes($"{AccessToken.Split('.')[0]}.{AccessToken.Split('.')[1]}"));

			}
		}
	}
}