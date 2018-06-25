using Newtonsoft.Json;

namespace Adapt.Model
{
    public class PageLevelProgressAdapt
    {
        [JsonProperty("_isEnabled")]
        public bool IsEnabled { get; set; }
    }
}
