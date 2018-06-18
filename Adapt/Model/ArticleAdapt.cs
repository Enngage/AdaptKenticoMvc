using System.Collections.Generic;
using CloudIntegration.Models;
using Newtonsoft.Json;

namespace Adapt.Model
{
    public class ArticleAdapt
    {
        [JsonIgnore]
        public List<Block> Blocks { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("_parentId")]
        public string ParentId { get; set; }

        [JsonProperty("_type")] public string Type = AdaptModelType.Article.Type;

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

    }
}
