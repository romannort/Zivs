using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zivs.OpenKey.Models
{
    public class OutputParams
    {
        [JsonProperty(PropertyName = "a")]
        public string A { get; set; }

        [JsonProperty(PropertyName = "b")]
        public string B { get; set; }

        [JsonProperty(PropertyName = "af")]
        public string Alpha { get; set; }

        [JsonProperty(PropertyName = "bt")]
        public string Beta { get; set; }

        [JsonProperty(PropertyName = "m")]
        public string Message { get; set; }
    }
}
