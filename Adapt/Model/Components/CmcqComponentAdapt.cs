using System.Collections.Generic;
using System.Linq;
using Adapt.Helpers;
using Adapt.Model.Types;
using CloudIntegration.Models.Cloud;
using Newtonsoft.Json;

namespace Adapt.Model.Components
{
    public class CmcqComponentAdapt : BaseAdaptComponent
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
        public List<CodeQuestionItem> Items { get; }

        [JsonProperty("_feedback")]
        public QuestionFeedback QuestionFeedback { get; }

        public override AdaptComponentType Component => AdaptComponentType.Cmcq;

        public CmcqComponentAdapt(string parentId, MultipleChoiceQuestionWithCode inputComponent) : base(parentId, inputComponent)
        {
            Body = inputComponent.QuestionText;
            Items = inputComponent.Answers?.Select(m => new CodeQuestionItem()
            {
                Title = m.Title,
                Code = new ComponentAdaptCode()
                {
                    Code = m.Code,
                    Lang = m.AvailableLanguagesLanguage.FirstOrDefault()?.Codename?.ToLower()
                },
                Feedback = m.IncorrectFeedback,
                ShouldBeSelected = YesOptionHelper.IsYesOptionChecked(m.IsThisAnswerCorrect)
            }).ToList();
            Selectable =
                inputComponent.Answers?.Count(m => YesOptionHelper.IsYesOptionChecked(m.IsThisAnswerCorrect)) ?? 0;
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
                }
            };
        }
     
    }

}
