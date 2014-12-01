using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


//string MyDictionaryToJson(Dictionary<int, List<int>> dict)
//{
//	var entries = dict.Select(d =>
//		string.Format("\"{0}\": [{1}]", d.Key, string.Join(",", d.Value)));
//	return "{" + string.Join(",", entries) + "}";
//}