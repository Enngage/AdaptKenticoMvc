
using System;
using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public partial class Package
    {
        public const string Codename = "package";
        public const string PagesCodename = "pages";
        public const string CourseNameCodename = "course_name";

        public IEnumerable<object> Pages { get; set; }
        public string CourseName { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}