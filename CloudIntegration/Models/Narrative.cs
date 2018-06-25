
using System;
using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public partial class Narrative : BaseComponent
    {
        public const string Codename = "narrative";
        public const string IntroCodename = "intro";
        public const string SmallInstructionsCodename = "small_instructions";
        public const string NarrativeItemsCodename = "narrative_items";
        public const string RequireAllItemsBeSeenCodename = "require_all_items_be_seen_";

        public string Intro { get; set; }
        public string SmallInstructions { get; set; }
        public IEnumerable<object> NarrativeItems { get; set; }
        public IEnumerable<MultipleChoiceOption> RequireAllItemsBeSeen { get; set; }
    }
}