using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adapt.Model;
using CloudIntegration;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    [Route(BaseConfig.MvcApiRoute)]
    public class GenerateController : Controller
    {
        private ICourseService CourseService { get; }
        private ICourseGenerateService CourseGenerateService { get; }

        public GenerateController(ICourseService courseService, ICourseGenerateService courseGenerateService)
        {
            CourseService = courseService;
            CourseGenerateService = courseGenerateService;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> ListAsync()
        {
            var courses = await CourseService.GetAllPackagesAsync();

            return new ObjectResult(courses);
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> IndexAsync([FromQuery] string courseId, bool debug, bool usePreview)
        {
            var generatedDataMessage = new List<string>();
            var debugContent = new List<AdaptCourseData>();

            var courseType = usePreview ? CourseFileTypeEnum.Preview : CourseFileTypeEnum.Prod;

            // generate data for course
            var result = await CourseGenerateService.GeneratePackageDataAsync(courseId, usePreview, courseType);
            generatedDataMessage.Add($"[{courseType}] Data for course '{result.Course.CourseName}' and version '{result.Course.CourseVersionVersion?.FirstOrDefault()?.Codename}' have been generated.");

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

        /// <summary>
        /// Web hook from KC
        /// </summary>
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
            // Or there could be more personalized web hooks coming in..
            var projectCourses = await CourseService.GetAllPackagesWithinProjectAsync(model.Message.ProjectId);

            // go through all courses within a project :-(
            foreach (var projectCourse in projectCourses)
            {
                var result = await CourseGenerateService.GeneratePackageDataAsync(projectCourse.CourseId, false, CourseFileTypeEnum.Prod);
                generatedDataMessage.Add($"Data for course '{result.Course.CourseName}' and version '{projectCourse.CourseVersionVersion.FirstOrDefault()?.Codename}' have been generated.");
            }

            return new ObjectResult(generatedDataMessage);
        }

  

    }
}
