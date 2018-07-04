using System.Collections.Generic;
using System.Threading.Tasks;
using CloudIntegration.Models;
using CloudIntegration.Models.Cloud;

namespace CloudIntegration
{
    public interface ICourseService
    {
        Task<List<PackageDto>> GetAllPackagesAsync();
        Task<List<Page>> GetPagesAsync(string projectId, string courseId);
        Task<List<Package>> GetAllCoursesWithinProjectAsync(string projectId);
        Task<Package> GetPackageAsync(string projectId, string courseId);
        Task<List<string>> GetPackageVersionsAsync(string projectId);
        List<Page> FilterPagesToIncludeOnlyItemsWithVersion(List<Page> pages, string courseVersion);

    }
}
