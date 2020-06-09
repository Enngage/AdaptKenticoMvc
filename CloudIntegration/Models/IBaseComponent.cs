using System.Collections.Generic;
using Kentico.Kontent.Delivery.Abstractions;
using Kentico.Kontent.Delivery.ContentItems;
using Kentico.Kontent.Delivery.ContentTypes.Element;
using Kentico.Kontent.Delivery.TaxonomyGroups;

namespace CloudIntegration.Models
{
    public interface IBaseComponent
    {
        IContentItemSystemAttributes System { get; set; }
        IEnumerable<TaxonomyTerm> ComponentClasses { get; set; }
        IEnumerable<MultipleChoiceOption> IsOptional { get; set; }
        string DisplayTitle { get; set; }
        IEnumerable<MultipleChoiceOption> Layout { get; set; }
        string Title { get; set; }
        string Instructions { get; set; }
        IEnumerable<MultipleChoiceOption> IncludeInProgress { get; set; }
        IEnumerable<MultipleChoiceOption> CourseVersion { get; set; }
    }
}
