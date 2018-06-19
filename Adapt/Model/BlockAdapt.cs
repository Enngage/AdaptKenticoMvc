using System.Collections.Generic;
using CloudIntegration.Models;
using Newtonsoft.Json;

namespace Adapt.Model
{
    public class BlockAdapt : BaseAdaptModel
    {
        [JsonIgnore]
        public List<IBaseComponent> Components { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("_trackingId")]
        public string TrackingId { get; set; }

        public override AdaptModelType Type { get; } = AdaptModelType.Block;
    }
}
