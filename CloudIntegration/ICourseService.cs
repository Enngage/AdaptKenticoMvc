using System.Collections.Generic;
using System.Threading.Tasks;
using CloudIntegration.Models;
using CloudIntegration.Models.Cloud;

namespace CloudIntegration
{
    public interface ICourseService
    {
        Task<List<PackageDto>> GetAllPackagesAsync();
        Task<List<Page>> GetPagesAsync(string courseId, bool usePreview);
        Task<List<Package>> GetAllPackagesWithinProjectAsync(string projectId);
        Task<Package> GetPackageAsync(string courseId, bool usePreview);
        Task<CourseServiceProject> GetProjectForCourseAsync(string courseId);
        List<Page> FilterPagesToIncludeOnlyItemsWithVersion(List<Page> pages, string courseVersion);
    }
}
