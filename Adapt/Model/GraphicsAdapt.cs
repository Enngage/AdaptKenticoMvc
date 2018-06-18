using System;
using Newtonsoft.Json;

namespace Adapt.Model
{
    public class GraphicsAdapt
    {
        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("alt")]
        public string Alt { get; set; }
    }
}
