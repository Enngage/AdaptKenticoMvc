
using System;
using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public partial class NarrativeCode : BaseComponent
    {
        public const string Codename = "narrative_code";
        public const string NarrativeCodeItemsCodename = "narrative_code_items";
        public const string SmallInstructionsCodename = "small_instructions";
        public const string RequireAllCodeBeSeeCodename = "require_all_code_be_see_";
        public const string IntroCodename = "intro";

        public IEnumerable<object> NarrativeCodeItems { get; set; }
        public string SmallInstructions { get; set; }
        public IEnumerable<MultipleChoiceOption> RequireAllCodeBeSee { get; set; }
        public string Intro { get; set; }
        public IEnumerable<TaxonomyTerm> BasecomponentCompontentClasses { get; set; }
    }
}