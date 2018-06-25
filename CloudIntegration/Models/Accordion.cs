
using System.Collections.Generic;

namespace CloudIntegration.Models
{
    public partial class Accordion :  BaseComponent
    {
        public const string Codename = "accordion";
        public const string AccordionItemsCodename = "accordion_items";
        public const string DescriptionCodename = "description";

        public IEnumerable<object> AccordionItems { get; set; }
        public string Description { get; set; }

    }
}