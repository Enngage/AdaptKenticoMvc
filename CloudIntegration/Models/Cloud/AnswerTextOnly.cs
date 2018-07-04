using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models.Cloud
{
    public partial class AnswerTextOnly
    {
        public const string Codename = "answer__text_only_";
        public const string IsThisACorrectAnswerCodename = "is_this_a_correct_answer_";
        public const string AnswerCodename = "answer";

        public IEnumerable<MultipleChoiceOption> IsThisACorrectAnswer { get; set; }
        public string Answer { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}