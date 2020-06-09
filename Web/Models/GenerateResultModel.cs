using Adapt.Model;
using KenticoKontentModels;

namespace Web.Models
{
    public class GenerateResultModel
    {
        public Package Course { get; set; }
        public AdaptCourseData CourseData { get; set; }
    }
}
