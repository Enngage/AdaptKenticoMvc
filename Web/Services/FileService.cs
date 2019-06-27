using System;
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

        public void CreateCourseJsonFiles(string courseId, string language, CourseFileTypeEnum type, AdaptCourseData courseData)
        {
            var courseDir = GetCourseFolder(courseId, language, type);

            LogGenerateAction(() =>
            {
                // make sure directory for course exists
                Directory.CreateDirectory(courseDir);

                // course data
                CreateJsonFile(courseDir, Config.ContentObjectsFilename, FixEmptyRichTextFields(JsonConvert.SerializeObject(courseData.Pages)));
                CreateJsonFile(courseDir, Config.ArticlesFilename, FixEmptyRichTextFields(JsonConvert.SerializeObject(courseData.Articles)));
                CreateJsonFile(courseDir, Config.BlocksFilename, FixEmptyRichTextFields(JsonConvert.SerializeObject(courseData.Blocks)));
                CreateJsonFile(courseDir, Config.ComponentsFilename, FixEmptyRichTextFields(JsonConvert.SerializeObject(courseData.Components)));

                // course config
                CreateJsonFile(courseDir, Config.CourseFilename, CombineDefaultAndCustomCourseConfig(courseData.Course));

            }, courseDir, courseData);
        }

        public string CombineDefaultAndCustomCourseConfig(AdaptCourseConfig courseConfig)
        {
            // load default data
            var defaultDataFilepath = GetDefaultDataFolder() + "\\" + Config.DefaultCourseJsonDataFilename;
            var defaultCourseData = JObject.Parse(File.ReadAllText(defaultDataFilepath));
            var customCourseData = JObject.FromObject(courseConfig);

            // set custom properties
            foreach (var property in customCourseData.Properties())
            {
                // overwrite default value
                defaultCourseData[property.Name] = property.Value;
            }

            return defaultCourseData.ToString();
        }

        public string FixEmptyRichTextFields(string text)
        {
            var result = text.Replace("\"<p><br></p>\"", "\"\"");

            return result;
        }

        public GenerateLogModel GetCourseLog(string courseId, string language, CourseFileTypeEnum type)
        {
            var courseDir = GetCourseFolder(courseId, language, type);
            var logFilePath = Path.Combine(courseDir, Config.CourseLogFilename);

            if (!File.Exists(logFilePath))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<GenerateLogModel>(File.ReadAllText(logFilePath));
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

        public string GetCourseFolder(string courseId, string language, CourseFileTypeEnum type)
        {
            var typeFolder = type == CourseFileTypeEnum.Preview ? "preview" : "prod";


            return $"{Config.RootFolder}\\{Config.CoursesFolderName}\\{typeFolder}\\{courseId.ToCodename()}\\{language}";
        }

        public string GetDefaultDataFolder()
        {
            return $"{Config.RootFolder}\\{Config.DefaultDataFolderName}";
        }

        private void LogGenerateAction(Action action, string courseDir, AdaptCourseData courseData)
        {
            action();

            CreateJsonFile(courseDir, Config.CourseLogFilename, JsonConvert.SerializeObject(new
                GenerateLogModel()
            {
                TimestampUTc = DateTime.UtcNow,
                CourseName = courseData.Course.Title,
                Articles = courseData.Articles.Count,
                Blocks = courseData.Blocks.Count,
                Components = courseData.Components.Count,
                Pages = courseData.Pages.Count
            }));
        }
    }
}
