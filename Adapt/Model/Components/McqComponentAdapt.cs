using System.Collections.Generic;
using System.Linq;
using Adapt.Helpers;
using Adapt.Model.Types;
using KenticoKontentModels;
using Newtonsoft.Json;

namespace Adapt.Model.Components
{
    public class McqComponentAdapt : BaseAdaptComponent
    {

        [JsonProperty("body")]
        public string Body { get; }

        [JsonProperty("_attempts")]
        public uint Attempts => 3;

        [JsonProperty("_questionWeight")]
        public uint QuestionWeight => 1;

        [JsonProperty("_canShowModelAnswer")]
        public bool CanShowModelAsnwer = true;

        [JsonProperty("_isRandom")]
        public bool IsRandom = true;

        /// <summary>
        /// Should contain sum of valid answers (i.e. if question has 2 correct answers, set it to 2...)
        /// </summary>
        [JsonProperty("_selectable")]
        public int Selectable { get; }

        [JsonProperty("_shouldDisplayAttempts")]
        public bool ShouldDisplayAttempts = true;

        [JsonProperty("_allowsPunctuation")]
        public bool AllowsPunctuation = true;

        [JsonProperty("_items")]
        public List<TextQuestionItem> Items { get; }

        [JsonProperty("_feedback")]
        public QuestionFeedback QuestionFeedback { get; }

        public override AdaptComponentType Component => AdaptComponentType.Mcq;

        public McqComponentAdapt(string parentId, MultipleChoiceQuestionTextOnly inputComponent) : base(parentId, inputComponent)
        {
            Body = inputComponent.QuestionText;
            Items = inputComponent.Answers?.Select(m => new TextQuestionItem()
            {
                Text = m.Answer,
                ShouldBeSelected = YesOptionHelper.IsYesOptionChecked(m.IsThisACorrectAnswer),
                Feedback = m.IncorrectFeedback
            }).ToList();
            Selectable =
                inputComponent.Answers?.Count(m => YesOptionHelper.IsYesOptionChecked(m.IsThisACorrectAnswer)) ?? 0;
            QuestionFeedback = new QuestionFeedback()
            {
                Title = inputComponent.FeedbackTitle,
                Correct = inputComponent.FeedbackIfCorrect,
                Incorrect = new QuestionFeedbackCorrectFinal()
                {
                    Final = inputComponent.FeedbackIfIncorrect
                },
                PartlyCorrect = new QuestionFeedbackCorrectFinal()
                {
                    Final = inputComponent.FeedbackIfPartlyCorrect
                },
            };
        }
     
    }
}
