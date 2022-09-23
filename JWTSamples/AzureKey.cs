using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json.Serialization;

namespace JWTSamples
{
	public class AzureKey
	{
		[JsonPropertyName("kty")]
		public string KeyType { get; set; }
		public string Use { get; set; }
		public string Kid { get; set; }
		public string X5t { get; set; }
		[JsonPropertyName("n")]
		public string Modulus { get; set; }
		[JsonPropertyName("e")]
		public string Exponent { get; set; }
		public List<string> X5c { get; set; }
		public string Issuer { get; set; }

	}
}