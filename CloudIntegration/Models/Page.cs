
using System;
using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public partial class Page
    {
        public const string Codename = "page";
        public const string InstructionsCodename = "instructions";
        public const string DurationCodename = "duration";
        public const string LinkTextCodename = "link_text";
        public const string TitleCodename = "title";
        public const string DisplayTitleCodename = "display_title";
        public const string SectionsCodename = "sections";
        public const string ImageCodename = "image";
        public const string TextCodename = "text";

        public string Instructions { get; set; }
        public decimal? Duration { get; set; }
        public string LinkText { get; set; }
        public string Title { get; set; }
        public string DisplayTitle { get; set; }
        public IEnumerable<Section> Sections { get; set; }
        public IEnumerable<Asset> Image { get; set; }
        public string Text { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}