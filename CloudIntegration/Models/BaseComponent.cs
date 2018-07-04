using System.Collections.Generic;
using KenticoCloud.Delivery;
using Newtonsoft.Json;

namespace CloudIntegration.Models
{
    public abstract class BaseComponent : IBaseComponent
    {
        public const string BasecomponentDisplayTitleCodename = "basecomponent__display_title";
        public const string BasecomponentIsOptionalCodename = "basecomponent__is_optional_";
        public const string BasecomponentLayoutCodename = "basecomponent__layout";
        public const string BasecomponentIncludeInProgressCodename = "basecomponent__include_in_progress_";
        public const string BasecomponentComponentClassesCodename = "basecomponent__component_classes";
        public const string BasecomponentTitleCodename = "basecomponent__title";
        public const string BasecomponentInstructionsCodename = "basecomponent__instructions";

        public ContentItemSystemAttributes System { get; set; }

        [JsonProperty(BasecomponentComponentClassesCodename)]
        public IEnumerable<TaxonomyTerm> ComponentClasses { get; set; }

        [JsonProperty(BasecomponentIsOptionalCodename)]
        public IEnumerable<MultipleChoiceOption> IsOptional { get; set; }

        [JsonProperty(BasecomponentDisplayTitleCodename)]
        public string DisplayTitle { get; set; }

        [JsonProperty(BasecomponentLayoutCodename)]
        public IEnumerable<MultipleChoiceOption> Layout { get; set; }

        [JsonProperty(BasecomponentTitleCodename)]
        public string Title { get; set; }

        [JsonProperty(BasecomponentInstructionsCodename)]
        public string Instructions { get; set; }

        [JsonProperty(BasecomponentIncludeInProgressCodename)]
        public IEnumerable<MultipleChoiceOption> IncludeInProgress { get; set; }

    }
}
