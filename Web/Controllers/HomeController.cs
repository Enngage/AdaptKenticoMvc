using System.Threading.Tasks;
using CloudIntegration;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private ICourseService CourseService { get; }

        public HomeController(ICourseService courseService)
        {
            CourseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await CourseService.GetAllPackagesAsync();

            return View(courses);
        }
    }
}