using System.Collections.Generic;
using CloudIntegration.Models;
using Newtonsoft.Json;

namespace Adapt.Model
{
    public class ArticleAdapt : BaseAdaptModel
    {
        [JsonIgnore]
        public List<Block> Blocks { get; set; }

        public override AdaptModelType Type { get; set; } = AdaptModelType.Article;

        [JsonProperty("body")]
        public string Body { get; set; }

    }
}
