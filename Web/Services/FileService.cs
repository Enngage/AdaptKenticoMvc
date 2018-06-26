using System.IO;
using Adapt.Model;
using Newtonsoft.Json;
using Web.Extensions;
using Web.Models;

namespace Web.Services
{
    public class FileService : IFileService
    {
        private static readonly object FileAccessLock = new object();

        private FileServiceConfig Config { get; }

        public FileService(FileServiceConfig config)
        {
            Config = config;
        }

        public void CreateCourseJsonFiles(string projectName, string language, AdaptCourseData courseData, string courseVersion)
        {
            var courseDir = GetCourseFolder(projectName, language, courseVersion);

            // make sure directory for course exists
            Directory.CreateDirectory(courseDir);

            CreateJsonFile(courseDir, Config.ContentObjectsFilename, JsonConvert.SerializeObject(courseData.Pages));
            CreateJsonFile(courseDir, Config.ArticlesFilename, JsonConvert.SerializeObject(courseData.Articles));
            CreateJsonFile(courseDir, Config.BlocksFilename, JsonConvert.SerializeObject(courseData.Blocks));
            CreateJsonFile(courseDir, Config.ComponentsFilename, JsonConvert.SerializeObject(courseData.Components));
        }

        public void CreateJsonFile(string folder, string filename, string content)
        {
            var filePath = folder + "\\" + filename;

            // lock access to files
            lock (FileAccessLock)
            {
                using (var file = File.CreateText(filePath))
                {
                    file.Write(content);
                }
            }
        }

        public string GetCourseFolder(string projectName, string language, string courseVersion = null)
        {
            return $"{Config.RootFolder}\\{Config.CoursesFolderName}\\{projectName.ToCodename()}\\{language}\\{(string.IsNullOrEmpty(courseVersion) ? Config.DefaultAllCourseDataFolder : courseVersion)}";
        }
    }
}
