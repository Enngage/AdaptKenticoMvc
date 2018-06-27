using Newtonsoft.Json;

namespace Adapt.Model
{
    public class SimpleGraphic
    {
        [JsonProperty("src")]
        public string Src { get; set; }

        [JsonProperty("alt")]
        public string Alt { get; set; }
    }

    public class FullGraphic
    {
        [JsonProperty("large")]
        public string LargeSrc { get; set; }

        [JsonProperty("small")]
        public string SmallSrc { get; set; }

        [JsonProperty("alt")] public string Alt { get; set; } = "";
    }

    public class MatchingItemOption
    {
        [JsonProperty("text")] public string Text { get; set; } = "Default question";

        [JsonProperty("_isCorrect")] public bool IsCorrect { get; set; } = false;
    }


    public class TextQuestionItem
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("_shouldBeSelected")] public bool ShouldBeSelected { get; set; } = false;
    }

    public class Feedback
    {
        [JsonProperty("title")]
        public string Title { get; set; } = "Feedback";

        [JsonProperty("correct")]
        public string Correct { get; set; }

        [JsonProperty("_partlyCorrect")]
        public TextQuestionFeedbackCorrectFinal PartlyCorrect { get; set; }

        [JsonProperty("_incorrect")]
        public TextQuestionFeedbackCorrectFinal Incorrect { get; set; }
    }

    public class TextQuestionFeedbackCorrectFinal
    {
        [JsonProperty("final")]
        public string Final { get; set; }
    }

    public class AccordionItemAdapt
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("_graphic")]
        public SimpleGraphic Graphic { get; set; }
    }
}
