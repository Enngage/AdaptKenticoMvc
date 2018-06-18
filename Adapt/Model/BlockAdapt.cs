using System.Collections.Generic;
using CloudIntegration.Models;
using Newtonsoft.Json;

namespace Adapt.Model
{
    public class BlockAdapt : BaseAdaptModel
    {
        [JsonIgnore]
        public List<Component> Components { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        public override AdaptModelType Type { get; set; } = AdaptModelType.Block;
    }
}
