using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public interface ICourseVersion
    {
        IEnumerable<MultipleChoiceOption> CourseVersionVersion { get; set; }
    }
}