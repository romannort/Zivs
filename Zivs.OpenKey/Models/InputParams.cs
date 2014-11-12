using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Zivs.OpenKey.Models
{
    public class InputParams
    {
        private static readonly char[] Separators = {' ', ','};

        private static readonly IList<string> Lib = new List<string>();
            
        [JsonProperty(PropertyName = "a")]
        public string A { get; set; }

        [JsonProperty(PropertyName = "b")]
        public string B { get; set; }

        [JsonProperty(PropertyName = "p")]
        public string P { get; set; }

        [JsonProperty(PropertyName = "s")]
        public string U { get; set; }

        [JsonProperty(PropertyName = "m")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "l")]
        public IList<string> Library
        {
            get
            {
                return Message == null
                    ? Lib
                    : Message.Split(Separators, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public bool Valid()
        {
            return ! (string.IsNullOrEmpty(A) ||
                 string.IsNullOrEmpty(B) ||
                 string.IsNullOrEmpty(P) ||
                 string.IsNullOrEmpty(U) ||
                 string.IsNullOrEmpty(Message)
                );
        }
    }
}
