using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adapt;
using CloudIntegration;
using Web.Models;

namespace Web.Services
{
    public class CourseGenerateService : ICourseGenerateService
    {
        private ICourseService CourseService { get; }
        private IAdaptService AdaptService { get; }
        private IFileService FileService { get; }

        public CourseGenerateService(ICourseService courseService, IAdaptService adaptService, IFileService fileService)
        {
            CourseService = courseService;
            AdaptService = adaptService;
            FileService = fileService;
        }

        public async Task<GenerateResultModel> GeneratePackageDataAsync(string courseId, bool usePreview, CourseFileTypeEnum type)
        {
            if (string.IsNullOrEmpty(courseId))
            {
                throw new NotSupportedException($"Please specify '{nameof(courseId)}' parameter");
            }

            var course = await CourseService.GetPackageAsync(courseId, usePreview);
            var pages = await CourseService.GetPagesAsync(courseId, usePreview);

            var courseData = AdaptService.GenerateCourseData(pages, course);

            // (re)generate course json files
            FileService.CreateCourseJsonFiles(course.CourseId, course.Language?.FirstOrDefault()?.Codename, type, courseData);

            return new GenerateResultModel()
            {
                Course = course,
                CourseData = courseData
            };
        }
    }
}
