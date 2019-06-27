using System.Collections.Generic;

namespace CloudIntegration
{
    public class CourseServiceConfig
    {
        public int Depth { get; }
        public List<CourseServiceProject> Projects { get; }

        public CourseServiceConfig(int depth, List<CourseServiceProject> projects)
        {
            Depth = depth;
            Projects = projects;
        }
    }
}
