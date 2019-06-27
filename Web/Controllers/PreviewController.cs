using System.Threading.Tasks;
using Adapt;
using Adapt.Model;
using CloudIntegration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Services;

namespace Web.Controllers
{
    [Route(BaseConfig.MvcApiRoute)]
    public class PreviewController : Controller
    {
        private ICourseService CourseService { get; }
        private IAdaptService AdaptService { get; }
        private IFileService FileService { get; }

        public PreviewController(ICourseService courseService, IAdaptService adaptService, IFileService fileService)
        {
            CourseService = courseService;
            AdaptService = adaptService;
            FileService = fileService;
        }

        [HttpGet]
        [Route("Course/{courseId}")]
        public async Task<IActionResult> CourseAsync(string courseId)
        {
            var courseData = await GetAdaptCourseDataAsync(courseId);

            return new JsonResult(FileService.FixEmptyRichTextFields(JsonConvert.SerializeObject(courseData.Course)));
        }

        [HttpGet]
        [Route("Articles/{courseId}")]
        public async Task<IActionResult> ArticlesAsync(string courseId)
        {
            var courseData = await GetAdaptCourseDataAsync(courseId);

            return new JsonResult(FileService.FixEmptyRichTextFields(JsonConvert.SerializeObject(courseData.Articles)));
        }

        [HttpGet]
        [Route("Blocks/{courseId}")]
        public async Task<IActionResult> BlocksAsync(string courseId)
        {
            var courseData = await GetAdaptCourseDataAsync(courseId);

            return new JsonResult(FileService.FixEmptyRichTextFields(JsonConvert.SerializeObject(courseData.Blocks)));
        }

        [HttpGet]
        [Route("Components/{courseId}")]
        public async Task<IActionResult> ComponentAsync(string courseId)
        {
            var courseData = await GetAdaptCourseDataAsync(courseId);

            return new JsonResult(FileService.FixEmptyRichTextFields(JsonConvert.SerializeObject(courseData.Components)));
        }

        [HttpGet]
        [Route("ContentObjects/{courseId}")]
        public async Task<IActionResult> ContentObjectsAsync(string courseId)
        {
            var courseData = await GetAdaptCourseDataAsync(courseId);

            return new JsonResult(FileService.FixEmptyRichTextFields(JsonConvert.SerializeObject(courseData.Pages)));
        }

        private async Task<AdaptCourseData> GetAdaptCourseDataAsync(string courseId)
        {
            var course = await CourseService.GetPackageAsync(courseId, true);
            var pages = await CourseService.GetPagesAsync(courseId, true);

            return AdaptService.GenerateCourseData(pages, course);
        }

    }
}
