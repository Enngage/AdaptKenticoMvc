using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration
{
    public interface ICourseVersion
    {
        IEnumerable<MultipleChoiceOption> CourseVersionVersion { get; set; }
    }
}