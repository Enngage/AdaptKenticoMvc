
using Newtonsoft.Json;

namespace Adapt.Model
{
    public class AccordionItemAdapt
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("title")] public string Title { get; set; }

        [JsonProperty("_graphic")]
        public SimpleGraphic Graphic { get; set; }

    }
}
