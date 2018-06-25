using System.Collections.Generic;
using Newtonsoft.Json;

namespace Adapt.Model.Components
{
    public class AccordionComponentAdapt : BaseAdaptComponent
    {

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("_items")]
        public List<AccordionItemAdapt> Items { get; set; }

        public override AdaptComponentType Component => AdaptComponentType.Accordion;
    }
}
