using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudIntegration.Models;
using CloudIntegration.Resolvers;
using KenticoCloud.Delivery;

namespace CloudIntegration
{
    public class CourseService : ICourseService
    {
        private string MasterProjectId { get; }

        public CourseService(string masterProjectId)
        {
            MasterProjectId = masterProjectId;
        }

        /// <summary>
        /// List of 'supported' courses. This is coming from a different Kentico Cloud project which lists all courses for an overview
        /// </summary>
        /// <returns></returns>
        public async Task<List<SupportedCourse>> GetSupportedCoursesAsync()
        {
            var deliveryClient = GetDeliveryClient(MasterProjectId);

            var courses = (await deliveryClient.GetItemsAsync<Course>(new EqualsFilter("system.type", Course.Codename)))
                .Items;

            var result = (await Task.WhenAll(courses.Select(async m => new SupportedCourse()
                {
                    Course = m,
                    Versions = await GetCourseVersionsAsync(m.Projectid)
                }))
            ).ToList();

            return result;
        }

        /// <summary>
        /// We need to filter out all objects that do not match our version because filtering does not work on 'modular_content' currently
        /// </summary>
        /// <returns></returns>
        public List<Page> FilterPagesToIncludeOnlyItemsWithVersion(List<Page> pages, string courseVersion)
        {
            return pages?.Where(page =>
            {
                page.Sections = page.Sections.Where(section =>
                {
                    section.Blocks = section.Blocks.Where(block => block.ContainsVersion(courseVersion)).ToList();
                    return section.ContainsVersion(courseVersion);
                });
                return page.ContainsVersion(courseVersion);
            }).ToList();
        }

        /// <summary>
        /// Gets pages and nested course data structure from Kentico Cloud
        /// </summary>
        /// <param name="projectId">ProjectId of course project</param>
        /// <param name="courseVersion">Course version. If none is specified, all versions are loaded</param>
        /// <returns></returns>
        public async Task<List<Page>> GetPagesAsync(string projectId, string courseVersion = null)
        {
            var deliveryClient = GetDeliveryClient(projectId, true);

            var queryParams = new List<IQueryParameter>()
            {
                new EqualsFilter("system.type", Page.Codename),
                new DepthParameter(5)
            };

            if (!string.IsNullOrEmpty(courseVersion))
            {
                queryParams.Add(new AnyFilter($"elements.{CourseVersion.CourseVersionVersionCodename}",
                    courseVersion));
            }

            var response = await deliveryClient.GetItemsAsync<Page>(queryParams);

            var pagesForGivenVersion = FilterPagesToIncludeOnlyItemsWithVersion(response.Items.ToList(), courseVersion);

            return pagesForGivenVersion;
        }

        public async Task<List<string>> GetCourseVersionsAsync(string projectId)
        {
            // Note: Ideally we would want to get content out of 'Content snippet', but that is not supported at this moment
            // Thats why query for 'Page' item which uses content snippet with version identifier
            return (await GetDeliveryClient(projectId)
                 .GetContentElementAsync(Page.Codename, CourseVersion.CourseVersionVersionCodename))
                .Options.Select(m => m.Codename).ToList();
        }

        /// <summary>
        /// Gets course metadata (i.e. language, course name..)
        /// </summary>
        public async Task<Package> GetCourseMetadataAsync(string projectId)
        {
            // there should be only single instance of course metadata per project. Thats why
            // we take only the first one.
            return (
                 await GetDeliveryClient(projectId).GetItemsAsync<Package>(new LimitParameter(1))
                ).Items
                .First();
        }

        /// <summary>
        /// Constructs delivery client
        /// </summary>
        private IDeliveryClient GetDeliveryClient(string projectId, bool waitForLoadingNewContent = false)
        {
            var client = new DeliveryClient(new DeliveryOptions()
            {
                ProjectId = projectId,
                WaitForLoadingNewContent = waitForLoadingNewContent
            })
            {
                CodeFirstModelProvider = { TypeProvider = new CustomTypeProvider() }
            };

            client.InlineContentItemsProcessor.RegisterTypeResolver(new InlineCodeResolver());

            return client;
        }
    }
}
