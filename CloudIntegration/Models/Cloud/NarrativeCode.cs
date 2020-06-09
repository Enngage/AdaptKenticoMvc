// This code was generated by a kontent-generators-net tool 
// (see https://github.com/Kentico/kontent-generators-net).
// 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
// For further modifications of the class, create a separate file with the partial class.

using System;
using System.Collections.Generic;
using CloudIntegration.Models;
using Kentico.Kontent.Delivery.Abstractions;

namespace KenticoKontentModels
{
    public partial class NarrativeCode : BaseComponent
    {
        public const string Codename = "narrative_code";
        public const string BasecomponentComponentClassesCodename = "basecomponent__component_classes";
        public const string BasecomponentCourseVersionCodename = "basecomponent__course_version";
        public const string BasecomponentDisplayTitleCodename = "basecomponent__display_title";
        public const string BasecomponentIncludeInProgressCodename = "basecomponent__include_in_progress_";
        public const string BasecomponentInstructionsCodename = "basecomponent__instructions";
        public const string BasecomponentIsOptionalCodename = "basecomponent__is_optional_";
        public const string BasecomponentLayoutCodename = "basecomponent__layout";
        public const string BasecomponentTitleCodename = "basecomponent__title";
        public const string IntroCodename = "intro";
        public const string NarrativeCodeItemsCodename = "narrative_code_items";
        public const string RequireAllCodeBeSeeCodename = "require_all_code_be_see_";
        public const string SmallInstructionsCodename = "small_instructions";

        public IEnumerable<ITaxonomyTerm> BasecomponentComponentClasses { get; set; }
        public IEnumerable<IMultipleChoiceOption> BasecomponentCourseVersion { get; set; }
        public string BasecomponentDisplayTitle { get; set; }
        public IEnumerable<IMultipleChoiceOption> BasecomponentIncludeInProgress { get; set; }
        public string BasecomponentInstructions { get; set; }
        public IEnumerable<IMultipleChoiceOption> BasecomponentIsOptional { get; set; }
        public IEnumerable<IMultipleChoiceOption> BasecomponentLayout { get; set; }
        public string BasecomponentTitle { get; set; }
        public string Intro { get; set; }
        public IEnumerable<NarrativeCodeItem> NarrativeCodeItems { get; set; }
        public IEnumerable<IMultipleChoiceOption> RequireAllCodeBeSee { get; set; }
        public string SmallInstructions { get; set; }
        public override IContentItemSystemAttributes System { get; set; }
    }
}