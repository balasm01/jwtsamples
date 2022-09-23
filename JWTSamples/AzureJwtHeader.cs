using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace JWTSamples
{
	public class AzureJwtHeader
	{
		[JsonProperty("typ")]
		public string Typ { get; set; }
		[JsonProperty("alg")]
		[JsonPropertyName("alg")]
		public string Algorithm { get; set; }
		[JsonProperty("x5t")]
		public string X5t { get; set; }
		[JsonProperty("kid")]
		public string Kid { get; set; }
	}
}