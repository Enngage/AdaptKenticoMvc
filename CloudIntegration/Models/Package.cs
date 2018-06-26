
using System;
using System.Collections.Generic;
using System.Linq;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public partial class Package
    {
        public const string Codename = "package";
        public const string PagesCodename = "pages";
        public const string CourseNameCodename = "course_name";
        public const string LanguageCodename = "language";

        public IEnumerable<object> Pages { get; set; }
        public string CourseName { get; set; }
        public IEnumerable<MultipleChoiceOption> Language { get; set; }
        public ContentItemSystemAttributes System { get; set; }

        public string CourseLanguageCodename => Language?.FirstOrDefault()?.Codename;
    }
}