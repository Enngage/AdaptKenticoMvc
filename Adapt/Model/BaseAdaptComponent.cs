
using Newtonsoft.Json;

namespace Adapt.Model
{
    public abstract class BaseAdaptComponent : BaseAdaptModel
    {
        public override AdaptModelType Type { get; } = AdaptModelType.Component;

        [JsonProperty("_component")] public string ComponentJson => Component.Type;

        [JsonIgnore]
        public abstract AdaptComponentType Component { get; }
    }
}
