using Newtonsoft.Json;

namespace Web.Models
{
    public class WebHookModel
    {
        [JsonProperty("message")]
        public WebHookMessageModel Message { get; set; }
    }

    public class WebHookMessageModel
    {
        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("operation")]
        public string Operation { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("api_name")]
        public string ApiName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
