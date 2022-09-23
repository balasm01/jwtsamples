using System;
using System.Text;

namespace JWTSamples
{
	public static class EncodingExtensions
	{
		public static string DecodeFromBase64String(string input)
		{
			input = input.Replace('_', '/').Replace('-', '+');
			switch (input.Length % 4)
			{
				case 2: input += "=="; break;
				case 3: input += "="; break;
			}
			return Encoding.UTF8.GetString(Convert.FromBase64String(input));
		}

		public static string EncodeToBase64String(string input)
		{
			return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
		}
	}
}