using System;
using Microsoft.IdentityModel.Tokens;

namespace JWTSamples
{
	public class User
	{
		public string Username { get; set; }
		public DateTime Created { get; set; }
		public DateTime Expiry { get; set; }
		public SecurityToken SecurityToken { get; set; }
		public string Token { get; set; }
	}
}