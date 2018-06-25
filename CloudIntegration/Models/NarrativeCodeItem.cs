
using System;
using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public partial class NarrativeCodeItem 
    {
        public const string Codename = "narrative_code_item";
        public const string TextCodename = "text";
        public const string AvailableLanguagesLanguageCodename = "available_languages__language";
        public const string CodeCodename = "code";
        public const string TitleCodename = "title";

        public ContentItemSystemAttributes System { get; set; }
        public string Text { get; set; }
        public IEnumerable<MultipleChoiceOption> AvailableLanguagesLanguage { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        
    }
}