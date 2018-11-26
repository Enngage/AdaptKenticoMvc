using Newtonsoft.Json;

namespace Adapt.Model
{
    public class AdaptCourseConfig
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("displayTitle")]
        public string DisplayTitle { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("_assessment")]
        public AdaptCourseConfigAssessment Assessment { get; set; }
    }

    public class AdaptCourseConfigAssessment
    {
        [JsonProperty("_scoreToPass")]
        public decimal? ScoreToPass { get; set; }

        [JsonProperty("_postTotalScoreToLms")]
        public bool PostTotalScoreToLms => true;

        [JsonProperty("_isPercentageBased")]
        public bool IsPercentageBased => true;
    }
}
