using System.Collections.Generic;
using System.Linq;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models.Cloud
{
    public partial class Package
    {
        public const string Codename = "package";
        public const string PagesCodename = "pages";
        public const string CourseNameCodename = "course_name";
        public const string LanguageCodename = "language";
        public const string DisplayTitleCodename = "display_title";
        public const string DescriptionCodename = "description";
        public const string ScoreToPassCodename = "score_to_pass";
        public const string BodyCodename = "score_to_pass";

        public IEnumerable<object> Pages { get; set; }
        public string CourseName { get; set; }
        public string DisplayTitle { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public int ScoreToPass { get; set; }
        public IEnumerable<MultipleChoiceOption> Language { get; set; }
        public ContentItemSystemAttributes System { get; set; }

        public string CourseLanguageCodename => Language?.FirstOrDefault()?.Codename;
    }
}