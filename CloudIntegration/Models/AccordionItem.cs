
using System;
using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public partial class AccordionItem 
    {
        public const string Codename = "accordion_item";
        public const string ImageCodename = "image";
        public const string TitleCodename = "title";
        public const string TextCodename = "text";

        public IEnumerable<Asset> Image { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}