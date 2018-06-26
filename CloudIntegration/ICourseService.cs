using System.Collections.Generic;
using System.Threading.Tasks;
using CloudIntegration.Models;

namespace CloudIntegration
{
    public interface ICourseService
    {
        Task<List<SupportedCourse.SupportedCourse>> GetSupportedCoursesAsync();
        Task<List<Page>> GetPagesAsync(string projectId, string courseVersion = null);
        Task<Package> GetCourseMetadataAsync(string projectId);
        Task<List<string>> GetCourseVersionsAsync(string projectId);
    }
}
