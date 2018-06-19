using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models.Components
{
    public class GraphicComponent : IBaseComponent
    {
        public const string Codename = "graphic_component";
        public const string SmallImageCodename = "small_image";
        public const string BasecomponentDisplayTitleCodename = "basecomponent__display_title";
        public const string BasecomponentLayoutCodename = "basecomponent__layout";
        public const string AltCodename = "alt";
        public const string AttributionCodename = "attribution";
        public const string BasecomponentTitleCodename = "basecomponent__title";
        public const string BasecomponentInstructionsCodename = "basecomponent__instructions";
        public const string LargeImageCodename = "large_image";

        public IEnumerable<Asset> SmallImage { get; set; }
        public string BasecomponentDisplayTitle { get; set; }
        public IEnumerable<MultipleChoiceOption> BasecomponentLayout { get; set; }
        public string Alt { get; set; }
        public string Attribution { get; set; }
        public string BasecomponentTitle { get; set; }
        public string BasecomponentInstructions { get; set; }
        public IEnumerable<Asset> LargeImage { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}
