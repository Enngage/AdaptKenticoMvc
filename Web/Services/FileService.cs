using System.IO;
using System.Threading.Tasks;
using Adapt.Model;
using Newtonsoft.Json;
using Web.Extensions;

namespace Web.Services
{
    public class FileService : IFileService
    {
        public const string CoursesFolderName = "courses";

        public const string ArticlesFilename = "articles.json";
        public const string ContentObjectsFilename = "contentObjects.json";
        public const string BlocksFilename = "blocks.json";
        public const string ComponentsFilename = "components.json";

        private string RootFolder { get; }

        public FileService(string rootFolder)
        {
            RootFolder = rootFolder;
        }

        public async Task CreateCourseJsonFilesAsync(string projectName, AdaptCourseData courseData)
        {
            var courseDir = GetCourseFolder(projectName);

            // make sure directory for course exists
            Directory.CreateDirectory(courseDir);

            await CreateJsonFileAsync(courseDir, ContentObjectsFilename, JsonConvert.SerializeObject(courseData.Pages));
            await CreateJsonFileAsync(courseDir, ArticlesFilename, JsonConvert.SerializeObject(courseData.Articles));
            await CreateJsonFileAsync(courseDir, BlocksFilename, JsonConvert.SerializeObject(courseData.Blocks));
            await CreateJsonFileAsync(courseDir, ComponentsFilename, JsonConvert.SerializeObject(courseData.Components));
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
            return $"{RootFolder}\\{CoursesFolderName}\\{projectName.ToCodename()}";
        }
    }
}
