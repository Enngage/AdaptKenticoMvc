using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration
{
    public interface IBaseComponent
    {
        ContentItemSystemAttributes System { get; set; }
        IEnumerable<TaxonomyTerm> ComponentClasses { get; set; }
        IEnumerable<MultipleChoiceOption> IsRequired { get; set; }
        string DisplayTitle { get; set; }
        IEnumerable<MultipleChoiceOption> Layout { get; set; }
        string Title { get; set; }
        string Instructions { get; set; }
        IEnumerable<MultipleChoiceOption> IncludeInProgress { get; set; }
    }
}
