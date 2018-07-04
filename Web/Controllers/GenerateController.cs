using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adapt;
using Adapt.Model;
using CloudIntegration;
using CloudIntegration.Models.Cloud;
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
            var courses = await CourseService.GetAllPackagesAsync();

            return new ObjectResult(courses);
        }

        [HttpGet]
        [Route("index")]
        [Route("")]
        public async Task<IActionResult> IndexAsync([FromQuery] string projectId, string courseId, bool debug)
        {
            var generatedDataMessage = new List<string>();
            var debugContent = new List<AdaptCourseData>();

            // generate data for course
            var result = await GeneratePackageDataAsync(projectId, courseId);
            generatedDataMessage.Add($"Data for course '{result.Course.CourseName}' and version'{result.Course.ActiveCourseVersion}' have been generated.");

            if (debug)
            {
                debugContent.Add(result.CourseData);
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

            // Generate course data for all courses and versions.
            // Ideally, we would want to regenerate only the item that changes (+ possibly children), but that
            // is not worth the effort right now + there is very limited possibility of identifying in which items certain item is included as modular content.
            // Or there could be more personalized webhooks coming in..
            var projectCourses = await CourseService.GetAllCoursesWithinProjectAsync(model.Message.ProjectId);

            // go through all courses within a project :-(
            foreach (var projectCourse in projectCourses)
            {
                var result = await GeneratePackageDataAsync(model.Message.ProjectId, projectCourse.CourseName);
                generatedDataMessage.Add($"Data for course '{result.Course.CourseName}' and version '{projectCourse.ActiveCourseVersion}' have been generated.");
            }

            return new ObjectResult(generatedDataMessage);
        }

        private async Task<GenerateResultModel> GeneratePackageDataAsync(string projectId, string courseId)
        {
            if (string.IsNullOrEmpty(projectId))
            {
                throw new NotSupportedException($"Please specify '{nameof(projectId)}' parameter");
            }

            if (string.IsNullOrEmpty(courseId))
            {
                throw new NotSupportedException($"Please specify '{nameof(courseId)}' parameter");
            }

            var package = await CourseService.GetPackageAsync(projectId, courseId);

            if (package == null)
            {
                throw new NullReferenceException($"Course with name '{courseId}' was not found in project '{projectId}'");
            }

            var course = await CourseService.GetPackageAsync(projectId, courseId);
            var pages = await CourseService.GetPagesAsync(projectId, courseId);

            var courseData = AdaptService.GenerateCourseData(pages, course);

            // (re)generate course json files
            FileService.CreateCourseJsonFiles(course.CourseId, course.CourseLanguageCodename, courseData);

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
