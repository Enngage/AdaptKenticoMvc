
using System;
using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public partial class Section
    {
        public const string Codename = "section";
        public const string BodyCodename = "body";
        public const string BlocksCodename = "blocks";
        public const string TitleCodename = "title";
        public const string DisplayTitleCodename = "display_title";

        public string Body { get; set; }
        public IEnumerable<Block> Blocks { get; set; }
        public string Title { get; set; }
        public string DisplayTitle { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}