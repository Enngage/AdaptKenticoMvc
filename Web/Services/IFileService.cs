using Adapt.Model;
using Web.Models;

namespace Web.Services
{
    public interface IFileService
    {
        void CreateCourseJsonFiles(string courseId, string language, CourseFileTypeEnum type, AdaptCourseData courseData);
        void CreateJsonFile(string folder, string filename, string content);
        string GetDefaultDataFolder();
        string GetCourseFolder(string courseId, string language, CourseFileTypeEnum type);
        string CombineDefaultAndCustomCourseConfig(AdaptCourseConfig courseConfig);
        string FixEmptyRichTextFields(string text);
        GenerateLogModel GetCourseLog(string courseId, string language, CourseFileTypeEnum type);


    }
}
