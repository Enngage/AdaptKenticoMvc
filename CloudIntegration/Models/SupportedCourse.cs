using System.Collections.Generic;
using CloudIntegration.Models.Cloud;

namespace CloudIntegration.Models
{
    public class SupportedCourse
    {
        public Course Course { get; set; }
        public List<string> Versions { get; set; }
    }
}
