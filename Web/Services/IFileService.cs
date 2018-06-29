using Adapt.Model;

namespace Web.Services
{
    public interface IFileService
    {
        void CreateCourseJsonFiles(string projectName, string language, AdaptCourseData courseData, string courseVersion = null);
        void CreateJsonFile(string folder, string filename, string content);
        string GetDefaultDataFolder();
        string GetCourseFolder(string projectId, string language, string courseVersion = null);
        string CombineDefaultAndCustomCourseConfig(AdaptCourseConfig courseConfig);
    }
}
