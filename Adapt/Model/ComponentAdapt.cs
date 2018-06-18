using Newtonsoft.Json;

namespace Adapt.Model
{
    public class ComponentAdapt : BaseAdaptModel
    {

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("_component")] public string ComponentJson => Component.Type;

        public override AdaptModelType Type { get; set; } = AdaptModelType.Component;

        [JsonIgnore]
        public AdaptComponentType Component { get; set; }
    }
}
