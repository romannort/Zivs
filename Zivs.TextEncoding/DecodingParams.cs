using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zivs.TextEncoding
{
	public class DecodingParams
	{
		[JsonProperty(PropertyName = "encode")]
		public bool Decode
		{
			get { return false; }
		}

		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		[JsonProperty(PropertyName = "abc")]
		public IDictionary<string, string> Alphabet { get; set; }

		[JsonProperty(PropertyName = "coef")]
		public string CompressionLevel { get; set; }

		public string ToJson()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}