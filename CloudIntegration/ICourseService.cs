using System.Collections.Generic;
using System.Threading.Tasks;
using CloudIntegration.Models;

namespace CloudIntegration
{
    public interface ICourseService
    {
        Task<List<Course>> GetSupportedCoursesAsync();
        Task<List<Page>> GetPagesAsync(string projectId);
        Task<Package> GetCourseMetadataAsync(string projectId);
    }
}
