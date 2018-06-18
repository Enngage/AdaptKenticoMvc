using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Adapt.Model
{
    public abstract class BaseAdaptModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_parentId")]
        public string ParentId { get; set; }

        [JsonProperty("_type")] public string TypeJson => Type.Type;

        [JsonIgnore]
        public abstract AdaptModelType Type { get; set; }
    }
}
