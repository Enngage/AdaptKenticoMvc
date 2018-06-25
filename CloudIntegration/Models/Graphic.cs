
using System;
using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public partial class Graphic : BaseComponent
    {
        public const string Codename = "graphic";
        public const string SmallImageCodename = "small_image";
        public const string AltCodename = "alt";
        public const string AttributionCodename = "attribution";
        public const string LargeImageCodename = "large_image";

        public IEnumerable<Asset> SmallImage { get; set; }
        public string Alt { get; set; }
        public string Attribution { get; set; }
        public IEnumerable<Asset> LargeImage { get; set; }

    }
}