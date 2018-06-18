using System.Collections.Generic;
using CloudIntegration.Models;
using Newtonsoft.Json;

namespace Adapt.Model
{
    public class PageAdapt : BaseAdaptModel
    {
        [JsonIgnore]
        public List<Article> Articles { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("displayTitle")]
        public string DisplayTitle { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("pageBody")]
        public string PageBody { get; set; }

        [JsonProperty("instructions")]
        public string Instructions { get; set; }

        [JsonProperty("duration")]
        public decimal? Duration { get; set; }

        [JsonProperty("linkText")]
        public string LinkText { get; set; }

        [JsonProperty("_graphics")]
        public GraphicsAdapt Graphics { get; set; }

        public override AdaptModelType Type { get; set; } = AdaptModelType.Page;
    }
}
