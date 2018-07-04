using KenticoCloud.Delivery;

namespace CloudIntegration.Models.Cloud
{
    public partial class Course
    {
        public const string Codename = "course";
        public const string ProjectidCodename = "projectid";
        public const string CourseNameCodename = "course_name";

        public string Projectid { get; set; }
        public string CourseName { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}
