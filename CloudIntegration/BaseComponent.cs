using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration
{
    public abstract class BaseComponent : IBaseComponent
    {
        public const string BasecomponentDisplayTitleCodename = "basecomponent__display_title";
        public const string BasecomponentIsRequiredCodename = "basecomponent__is_required_";
        public const string BasecomponentLayoutCodename = "basecomponent__layout";
        public const string BasecomponentIncludeInProgressCodename = "basecomponent__include_in_progress_";
        public const string BasecomponentComponentClassesCodename = "basecomponent__component_classes";
        public const string BasecomponentTitleCodename = "basecomponent__title";
        public const string BasecomponentInstructionsCodename = "basecomponent__instructions";

        public  IEnumerable<TaxonomyTerm> BasecomponentComponentClasses { get; set; }
        public  IEnumerable<MultipleChoiceOption> BasecomponentIsRequired { get; set; }
        public  string BasecomponentDisplayTitle { get; set; }
        public  IEnumerable<MultipleChoiceOption> BasecomponentLayout { get; set; }
        public  string BasecomponentTitle { get; set; }
        public  string BasecomponentInstructions { get; set; }
        public  ContentItemSystemAttributes System { get; set; }
        public  IEnumerable<MultipleChoiceOption> BasecomponentIncludeInProgress { get; set; }

    }
}
