using Newtonsoft.Json;

namespace JWTSamples
{
	public static class JsonExtensions
	{
		public static string AsJsonString(this object o)
		{
			return JsonConvert.SerializeObject(o, Formatting.Indented);
		}
	}
}