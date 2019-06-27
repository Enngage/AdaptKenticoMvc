using System;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Services
{
    public interface ICourseGenerateService
    {
        Task<GenerateResultModel> GeneratePackageDataAsync(string courseId, bool usePreview, CourseFileTypeEnum type);
    }
}
