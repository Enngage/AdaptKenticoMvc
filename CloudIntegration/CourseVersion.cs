using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration
{
    public abstract class CourseVersion : ICourseVersion
    {
        public const string CourseVersionVersionCodename = "course_version__version";

        public IEnumerable<MultipleChoiceOption> CourseVersionVersion { get; set; }
    }
}
