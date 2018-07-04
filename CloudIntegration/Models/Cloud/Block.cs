using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models.Cloud
{
    public partial class Block
    {
        public const string Codename = "block";
        public const string DisplayTitleCodename = "display_title";
        public const string BodyCodename = "body";
        public const string TitleCodename = "title";
        public const string ComponentsCodename = "components";

        public string DisplayTitle { get; set; }
        public string Body { get; set; }
        public string Title { get; set; }
        public List<object> Components { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}