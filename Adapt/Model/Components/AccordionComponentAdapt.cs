using System.Collections.Generic;
using System.Linq;
using Adapt.Helpers;
using CloudIntegration.Models;
using Newtonsoft.Json;

namespace Adapt.Model.Components
{
    public class AccordionComponentAdapt : BaseAdaptComponent
    {

        [JsonProperty("body")]
        public string Body { get; }

        [JsonProperty("_items")]
        public List<AccordionItemAdapt> Items { get; }

        public override AdaptComponentType Component => AdaptComponentType.Accordion;

        public AccordionComponentAdapt(string parentId, Accordion inputComponent) : base(parentId, inputComponent)
        {
            Body = inputComponent.Description;
            Items = inputComponent.AccordionItems.Select(m => new AccordionItemAdapt()
            {
                Graphic = GraphicHelper.GetSimpleGraphic(m.Image),
                Title = m.Title,
                Body = m.Text
            }).ToList();
        }
    }
}
