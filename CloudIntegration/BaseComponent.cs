using System;
using System.Collections.Generic;
using System.Text;
using KenticoCloud.Delivery;

namespace CloudIntegration
{
    public abstract class BaseComponent : IBaseComponent
    {
        public string BasecomponentDisplayTitle { get; set; }
        public IEnumerable<MultipleChoiceOption> BasecomponentLayout { get; set; }
        public string BasecomponentTitle { get; set; }
        public string BasecomponentInstructions { get; set; }


        public abstract ContentItemSystemAttributes System { get; set; }
    }
}
