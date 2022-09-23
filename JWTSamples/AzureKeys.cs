using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JWTSamples
{
	public class AzureKeys
	{
		[JsonPropertyName("keys")]
		public List<AzureKey> Keys { get; set; }
	}
}