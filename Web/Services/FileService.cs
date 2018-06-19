using System.IO;
using System.Threading.Tasks;
using Adapt.Model;
using Newtonsoft.Json;
using Web.Extensions;
using Web.Models;

namespace Web.Services
{
    public class FileService : IFileService
    {
        private FileServiceConfig Config { get; }

        public FileService(FileServiceConfig config)
        {
            Config = config;
        }

        public async Task CreateCourseJsonFilesAsync(string projectName, AdaptCourseData courseData)
        {
            var courseDir = GetCourseFolder(projectName);

            // make sure directory for course exists
            Directory.CreateDirectory(courseDir);

            await CreateJsonFileAsync(courseDir, Config.ContentObjectsFilename, JsonConvert.SerializeObject(courseData.Pages));
            await CreateJsonFileAsync(courseDir, Config.ArticlesFilename, JsonConvert.SerializeObject(courseData.Articles));
            await CreateJsonFileAsync(courseDir, Config.BlocksFilename, JsonConvert.SerializeObject(courseData.Blocks));
            await CreateJsonFileAsync(courseDir, Config.ComponentsFilename, JsonConvert.SerializeObject(courseData.Components));
        }

        public async Task CreateJsonFileAsync(string folder, string filename, string content)
        {
            var filePath = folder + "\\" + filename;

            using (var file = File.CreateText(filePath))
            {
                await file.WriteAsync(content);
            }
        }

        public string GetCourseFolder(string projectName)
        {
            return $"{Config.RootFolder}\\{Config.CoursesFolderName}\\{projectName.ToCodename()}";
        }
    }
}
