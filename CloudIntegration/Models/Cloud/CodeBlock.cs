using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models.Cloud
{
    public partial class CodeBlock
    {
        public const string Codename = "code_block";
        public const string AvailableLanguagesLanguageCodename = "available_languages__language";
        public const string CodeCodename = "code";

        public IEnumerable<MultipleChoiceOption> AvailableLanguagesLanguage { get; set; }
        public string Code { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}