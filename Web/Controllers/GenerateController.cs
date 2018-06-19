using System.Linq;
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
        [Route("List")]
        public async Task<IActionResult> List()
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
        public async Task<IActionResult> Index([FromQuery] string projectId, bool debug = false)
        {
            var course = await CourseService.GetCourseMetadataAsync(projectId);
            var pages = await CourseService.GetPagesAsync(projectId);

            var courseData = AdaptService.GenerateCourseData(pages);

            // (re)generate course json files
            await FileService.CreateCourseJsonFilesAsync(course.CourseName, courseData);

            if (debug)
            {
                return new ObjectResult(courseData);
            }

            return new ObjectResult($"Data for course '{course.CourseName}' with projectId '{projectId}' have been generated.");
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

    }
}
