using Newtonsoft.Json;

namespace Adapt.Model
{
    public class FullGraphic
    {
        [JsonProperty("large")]
        public string LargeSrc { get; set; }

        [JsonProperty("small")]
        public string SmallSrc { get; set; }

        [JsonProperty("alt")]
        public string Alt { get; set; }
    }
}
