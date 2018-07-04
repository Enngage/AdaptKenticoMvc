using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models.Cloud
{
    public partial class NarrativeItem 
    {
        public const string Codename = "narrative_item";
        public const string TextCodename = "text";
        public const string ImageCodename = "image";
        public const string TitleCodename = "title";

        public string Text { get; set; }
        public IEnumerable<Asset> Image { get; set; }
        public string Title { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}