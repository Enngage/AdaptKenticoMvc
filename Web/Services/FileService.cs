using System.IO;
using Adapt.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public void CreateCourseJsonFiles(string courseId, string language, AdaptCourseData courseData)
        {
            var courseDir = GetCourseFolder(courseId, language);

            // make sure directory for course exists
            Directory.CreateDirectory(courseDir);

            // course data
            CreateJsonFile(courseDir, Config.ContentObjectsFilename, JsonConvert.SerializeObject(courseData.Pages));
            CreateJsonFile(courseDir, Config.ArticlesFilename, JsonConvert.SerializeObject(courseData.Articles));
            CreateJsonFile(courseDir, Config.BlocksFilename, JsonConvert.SerializeObject(courseData.Blocks));
            CreateJsonFile(courseDir, Config.ComponentsFilename, JsonConvert.SerializeObject(courseData.Components));

            // course config
            CreateJsonFile(courseDir, Config.CourseFilename, CombineDefaultAndCustomCourseConfig(courseData.Course));
        }

        public string CombineDefaultAndCustomCourseConfig(AdaptCourseConfig courseConfig)
        {
            // load default data
            var defaultDataFilepath = GetDefaultDataFolder() + "\\" + Config.DefaultCourseJsonDataFilename;
            var defaultCourseData =  JObject.Parse(File.ReadAllText(defaultDataFilepath));
            var customCourseData = JObject.FromObject(courseConfig);

            // set custom properties
            foreach (var property in customCourseData.Properties())
            {
                // overwrite default value
                defaultCourseData[property.Name] = property.Value;
            }

            return defaultCourseData.ToString();
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

        public string GetCourseFolder(string courseId, string language)
        {
            return $"{Config.RootFolder}\\{Config.CoursesFolderName}\\{courseId.ToCodename()}\\{language}";
        }

        public string GetDefaultDataFolder()
        {
            return $"{Config.RootFolder}\\{Config.DefaultDataFolderName}";
        }
   
    }
}
