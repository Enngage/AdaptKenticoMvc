using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adapt;
using Adapt.Model;
using CloudIntegration;
using CloudIntegration.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    [Route(BaseConfig.MvcApiRoute)]
    public class GenerateController : Controller
    {
        private ICourseService CourseService { get; }
        private IAdaptService AdaptService { get; }
        private IFileService FileService { get; }

        public GenerateController(ICourseService courseService, IAdaptService adaptService, IFileService fileService)
        {
            CourseService = courseService;
            AdaptService = adaptService;
            FileService = fileService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ListAsync()
        {
            var courses = await CourseService.GetSupportedCoursesAsync();

            return new ObjectResult(courses);
        }

        [HttpGet]
        [Route("index")]
        [Route("")]
        public async Task<IActionResult> IndexAsync([FromQuery] string projectId, string version, bool debug)
        {
            var generatedDataMessage = new List<string>();
            var debugContent = new List<AdaptCourseData>();

            if (string.IsNullOrEmpty(version))
            {
                // generate data for all versions
                var courseVersions = await CourseService.GetCourseVersionsAsync(projectId);
                foreach (var courseVersion in courseVersions)
                {
                    var result = await GenerateCourseDataAsync(projectId, courseVersion);
                    generatedDataMessage.Add($"Data for course '{result.Course.CourseName}' and version'{courseVersion}' have been generated.");

                    if (debug)
                    {
                        debugContent.Add(result.CourseData);
                    }
                }
            }
            else
            {
                // generate for specified version
                var result = await GenerateCourseDataAsync(projectId, version);
                generatedDataMessage.Add($"Data for course '{result.Course.CourseName}' and version'{version}' have been generated.");

                if (debug)
                {
                    debugContent.Add(result.CourseData);
                }
            }

            return new ObjectResult(new
            {
                Result = generatedDataMessage,
                Debug = debugContent
            });
        }

        [HttpPost]
        [Route("UpdateCourse")]
        public async Task<IActionResult> UpdateCourseAsync([FromBody] WebHookModel model)
        {
            if (model == null)
            {
                throw new NotSupportedException($"Invalid web hook model");
            }

            var generatedDataMessage = new List<string>();

            // Generate course data for all versions of the course.
            // Ideally, we would want to regenerate only the item that changes (+ possibly children), but that
            // is not worth the effort right now.
            var courseVersions = await CourseService.GetCourseVersionsAsync(model.Message.ProjectId);
            foreach (var courseVersion in courseVersions)
            {
                var result = await GenerateCourseDataAsync(model.Message.ProjectId, courseVersion);
                generatedDataMessage.Add($"Data for course '{result.Course.CourseName}' and version'{courseVersion}' have been generated.");
            }

            return new ObjectResult(generatedDataMessage);
        }


        private async Task<GenerateResultModel> GenerateCourseDataAsync(string projectId, string courseVersion = null)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                throw new ArgumentNullException(nameof(projectId));
            }

            var course = await CourseService.GetCoursePackageAsync(projectId);
            var pages = await CourseService.GetPagesAsync(projectId, courseVersion);

            var courseData = AdaptService.GenerateCourseData(pages, course);

            // (re)generate course json files
            FileService.CreateCourseJsonFiles(course.CourseName, course.CourseLanguageCodename, courseData, courseVersion);

            return new GenerateResultModel()
            {
                Course = course,
                CourseData = courseData
            };
        }

        private class GenerateResultModel
        {
            public Package Course { get; set; }
            public AdaptCourseData CourseData { get; set; }
        }

    }
}
