using System.Collections.Generic;
using CloudIntegration.Models;

namespace CloudIntegration
{
    public class SupportedCourse
    {
        public Course Course { get; set; }
        public List<string> Versions { get; set; }
    }
}
