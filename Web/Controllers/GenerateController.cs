using System.Threading.Tasks;
using CloudIntegration;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route(BaseConfig.MvcApiRoute)]
    public class GenerateController : Controller
    {
        private IDataService DataService { get; }

        public GenerateController(IDataService dataService)
        {
            DataService = dataService;
        }

        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> Test([FromQuery] string projectId)
        {
            var articles = await DataService.GetArticlesAsync(projectId);

            return new ObjectResult(articles);
        }

        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

    }
}
