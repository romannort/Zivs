using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zivs.TextEncoding
{
	public class EncodingParams
	{
		[JsonProperty(PropertyName = "encode")]
		public bool Encode { get { return true; } }

		[JsonProperty(PropertyName = "text")]
		public string Text { get; set; }

		public string ToJson()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
