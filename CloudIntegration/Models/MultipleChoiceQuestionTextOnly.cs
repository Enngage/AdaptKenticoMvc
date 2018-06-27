
using System.Collections.Generic;

namespace CloudIntegration.Models
{
    public partial class MultipleChoiceQuestionTextOnly : BaseComponent
    {
        public const string Codename = "multiple_choice_question__text_only_";
        public const string FeedbackIfPartlyCorrectCodename = "feedback_if_partly_correct";
        public const string QuestionTextCodename = "question_text";
        public const string AnswersCodename = "answers";
        public const string FeedbackIfIncorrectCodename = "feedback_if_incorrect";
        public const string FeedbackIfCorrectCodename = "feedback_if_correct";

        public string FeedbackIfPartlyCorrect { get; set; }
        public string QuestionText { get; set; }
        public IEnumerable<AnswerTextOnly> Answers { get; set; }
        public string FeedbackIfIncorrect { get; set; }
        public string FeedbackIfCorrect { get; set; }

     
    }
}