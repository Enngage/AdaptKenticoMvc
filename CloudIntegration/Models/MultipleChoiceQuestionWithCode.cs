
using System.Collections.Generic;

namespace CloudIntegration.Models
{
    public partial class MultipleChoiceQuestionWithCode : BaseComponent
    {
        public const string Codename = "multiple_choice_question__with_code_";
        public const string QuestionTextCodename = "question_text";
        public const string FeedbackIfCorrectCodename = "feedback_if_correct";
        public const string FeedbackIfPartlyCorrectCodename = "feedback_if_partly_correct";
        public const string AnswersCodename = "answers";
        public const string FeedbackIfIncorrectCodename = "feedback_if_incorrect";

        public string QuestionText { get; set; }
        public string FeedbackIfCorrect { get; set; }
        public string FeedbackIfPartlyCorrect { get; set; }
        public IEnumerable<AnswerCode> Answers { get; set; }
        public string FeedbackIfIncorrect { get; set; }

    }
}