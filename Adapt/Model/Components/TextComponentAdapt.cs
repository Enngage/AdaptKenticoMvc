using Newtonsoft.Json;

namespace Adapt.Model.Components
{
    public class TextComponentAdapt : BaseAdaptComponent
    {

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("instruction")]
        public string Instructions { get; set; }

        [JsonProperty("_layout")]
        public string Layout { get; set; }

        public override AdaptComponentType Component => AdaptComponentType.Text;
    }
}
