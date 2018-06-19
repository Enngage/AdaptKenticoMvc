using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public interface IBaseComponent
    {
         string BasecomponentDisplayTitle { get; set; }
         IEnumerable<MultipleChoiceOption> BasecomponentLayout { get; set; }
         string BasecomponentTitle { get; set; }
         string BasecomponentInstructions { get; set; }
         ContentItemSystemAttributes System { get; set; }
    }
}
