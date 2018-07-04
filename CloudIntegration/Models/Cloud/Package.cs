using System.Collections.Generic;
using System.Linq;
using KenticoCloud.Delivery;
using Newtonsoft.Json;

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
        public const string CourseVersionCodename = "course_version__version";
        public const string CourseIdCodename = "course_id";
        public const string NarrativeNavigationPlacementCodename = "narrative_navigation_placement";

        public IEnumerable<Page> Pages { get; set; }
        public string CourseName { get; set; }
        public string DisplayTitle { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string CourseId { get; set; }
        public int ScoreToPass { get; set; }
        public IEnumerable<MultipleChoiceOption> Language { get; set; }
        public ContentItemSystemAttributes System { get; set; }
        public IEnumerable<MultipleChoiceOption> NarrativeNavigationPlacement { get; set; }

        [JsonProperty(CourseVersionCodename)]
        public IEnumerable<MultipleChoiceOption> CourseVersion { get; set; }

        public string CourseLanguageCodename => Language?.FirstOrDefault()?.Codename;

        public string ActiveCourseVersion => CourseVersion.FirstOrDefault()?.Codename;
    }
}