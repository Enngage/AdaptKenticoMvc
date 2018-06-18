using System.Threading.Tasks;
using Adapt.Model;

namespace Web.Services
{
    public interface IFileService
    {
        Task CreateCourseJsonFilesAsync(string projectName, AdaptCourseData courseData);
        Task CreateJsonFileAsync(string folder, string filename, string content);
        string GetCourseFolder(string projectId);
    }
}
