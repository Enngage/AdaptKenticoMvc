using Newtonsoft.Json;

namespace Adapt.Model.Components
{
    public class TextComponentAdapt : BaseAdaptComponent
    {

        [JsonProperty("body")]
        public string Body { get; set; }

        public override AdaptComponentType Component => AdaptComponentType.Text;
    }
}
