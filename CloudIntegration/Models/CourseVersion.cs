using System;
using System.Collections.Generic;
using System.Linq;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public abstract class CourseVersion : ICourseVersion
    {
        public const string CourseVersionVersionCodename = "course_version__version";

        public IEnumerable<MultipleChoiceOption> CourseVersionVersion { get; set; }

        public bool ContainsVersion(string version)
        {
            return CourseVersionVersion?.FirstOrDefault(m =>
                       m.Codename.Equals(version, StringComparison.OrdinalIgnoreCase)) != null;
        }
    }
}
