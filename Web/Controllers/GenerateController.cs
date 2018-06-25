using System;
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

            var courseResponse = courses.Select(m => new
            {
                CourseName = m.CourseName,
                ProjectId = m.Projectid
            });

            return new ObjectResult(courseResponse);
        }

        [HttpGet]
        [Route("index")]
        [Route("")]
        public async Task<IActionResult> IndexAsync([FromQuery] string projectId, bool debug = false)
        {
            var result = await GenerateCourseDataAsync(projectId);

            if (debug)
            {
                return new ObjectResult(result.CourseData);
            }

            return new ObjectResult($"Data for course '{result.Course.CourseName}' with projectId '{projectId}' have been generated.");
        }

        [HttpPost]
        [Route("UpdateCourse")]
        public async Task<IActionResult> UpdateCourseAsync([FromBody] WebHookModel model)
        {
            if (model == null)
            {
                throw new NotSupportedException($"Invalid web hook model");
            }

            var result = await GenerateCourseDataAsync(model.Message.ProjectId);

            return new ObjectResult($"Data for course '{result.Course.CourseName}' with projectId '{model.Message.ProjectId}' have been generated.");
        }


        private async Task<GenerateResultModel> GenerateCourseDataAsync(string projectId)
        {
            var course = await CourseService.GetCourseMetadataAsync(projectId);
            var pages = await CourseService.GetPagesAsync(projectId);

            var courseData = AdaptService.GenerateCourseData(pages);

            // (re)generate course json files
            FileService.CreateCourseJsonFiles(course.CourseName, courseData);

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
