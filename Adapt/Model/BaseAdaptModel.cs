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

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("displayTitle")]
        public string DisplayTitle { get; set; }

        [JsonIgnore]
        public abstract AdaptModelType Type { get;  }
    }
}
