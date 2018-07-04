using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models.Cloud
{
    public partial class AnswerCode
    {
        public const string Codename = "answer__code_";
        public const string TitleCodename = "title";
        public const string AvailableLanguagesLanguageCodename = "available_languages__language";
        public const string IsThisAnswerCorrectCodename = "is_this_answer_correct_";
        public const string CodeCodename = "code";
        public const string IncorrectFeedbackCodename = "incorrect_feedback";

        public string Title { get; set; }
        public IEnumerable<MultipleChoiceOption> AvailableLanguagesLanguage { get; set; }
        public IEnumerable<MultipleChoiceOption> IsThisAnswerCorrect { get; set; }
        public string Code { get; set; }
        public string IncorrectFeedback { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}