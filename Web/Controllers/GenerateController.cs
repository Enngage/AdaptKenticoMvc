using System.Threading.Tasks;
using Adapt;
using CloudIntegration;
using Microsoft.AspNetCore.Mvc;
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
        [Route("")]
        public async Task<IActionResult> Index([FromQuery] string projectId)
        {
            var course = await CourseService.GetCourseMetadataAsync(projectId);
            var pages = await CourseService.GetPagesAsync(projectId);

            var courseData = AdaptService.GenerateCourseData(pages);

            // (re)generate course json files
            await FileService.CreateCourseJsonFilesAsync(course.CourseName, courseData);

            return new ObjectResult(courseData);
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

    }
}
