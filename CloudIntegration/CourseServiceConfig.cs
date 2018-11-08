using System.Collections.Generic;

namespace CloudIntegration
{
    public class CourseServiceConfig
    {
        public int Depth { get; }
        public List<string> AllProjectIds { get; }

        public CourseServiceConfig(int depth, List<string> allProjectIds)
        {
            Depth = depth;
            AllProjectIds = allProjectIds;
        }
    }
}
