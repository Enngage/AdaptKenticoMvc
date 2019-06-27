using Adapt.Model;
using CloudIntegration.Models.Cloud;

namespace Web.Models
{
    public class GenerateResultModel
    {
        public Package Course { get; set; }
        public AdaptCourseData CourseData { get; set; }
    }
}
