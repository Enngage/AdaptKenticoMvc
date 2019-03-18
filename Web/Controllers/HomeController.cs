using System.Linq;
using System.Threading.Tasks;
using CloudIntegration;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Services;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private ICourseService CourseService { get; }
        private IFileService FileService { get; }

        public HomeController(ICourseService courseService, IFileService fileService)
        {
            CourseService = courseService;
            FileService = fileService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await CourseService.GetAllPackagesAsync();

            return View(courses.Select(m => new SupportedPackageDto()
            {
                Log = FileService.GetCourseLog(m.Package.CourseId, m.Package.Language.First().Codename),
                Package = m.Package,
                ProjectId = m.ProjectId
            }).ToList());
        }
    }
}