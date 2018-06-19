using Adapt.Model;

namespace Web.Services
{
    public interface IFileService
    {
        void CreateCourseJsonFiles(string projectName, AdaptCourseData courseData);
        void CreateJsonFile(string folder, string filename, string content);
        string GetCourseFolder(string projectId);
    }
}
