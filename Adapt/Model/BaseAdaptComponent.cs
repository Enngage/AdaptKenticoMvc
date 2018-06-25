
using Newtonsoft.Json;

namespace Adapt.Model
{
    public abstract class BaseAdaptComponent : BaseAdaptModel
    {
        public override AdaptModelType Type { get; } = AdaptModelType.Component;

        [JsonProperty("instruction")]
        public string Instructions { get; set; }

        [JsonProperty("_layout")]
        public string Layout { get; set; }

        [JsonProperty("_component")] public string ComponentJson => Component.Type;

        [JsonProperty("_pageLevelProgress")]
        public PageLevelProgressAdapt PageLevelProgress { get; set; }

        [JsonProperty("_isOptional")]
        public bool IsOptional { get; set; }

        [JsonProperty("_classes")]
        public string Classes { get; set; }

        [JsonIgnore]
        public abstract AdaptComponentType Component { get; }
    }
}
