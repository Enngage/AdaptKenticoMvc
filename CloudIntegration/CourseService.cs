using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudIntegration.Models;
using CloudIntegration.Models.Cloud;
using CloudIntegration.Resolvers;
using KenticoCloud.Delivery;

namespace CloudIntegration
{
    public class CourseService : ICourseService
    {
        private CourseServiceConfig Config { get; }

        public CourseService(CourseServiceConfig config)
        {
            Config = config;
        }

        /// <summary>
        /// List of 'supported' courses
        /// </summary>
        public async Task<List<PackageDto>> GetAllPackagesAsync()
        {
            var packages = new List<PackageDto>();

            foreach (var project in Config.Projects)
            {
                var deliveryClient = GetDeliveryClient(project.ProjectId, false, project.PreviewApiKey);

                packages.AddRange((await deliveryClient.GetItemsAsync<Package>()).Items.Select(m => new PackageDto()
                {
                    Package = m,
                    ProjectId = project.ProjectId
                }));
            }

            return packages;
        }

        public async Task<Package> GetPackageAsync(string courseId, bool usePreview)
        {
            var projectForCourse = await GetProjectForCourseAsync(courseId);

            return (
                    await GetDeliveryClient(projectForCourse.ProjectId, usePreview, projectForCourse.PreviewApiKey).GetItemsAsync<Package>(
                        new LimitParameter(1),
                        new EqualsFilter($"elements.{Package.CourseIdCodename}", courseId)
                    )
                ).Items
                .FirstOrDefault() ?? throw new NullReferenceException($"Course with id '{courseId}' in project '{projectForCourse.ProjectId}' was not found");
        }

        public async Task<CourseServiceProject> GetProjectForCourseAsync(string courseId)
        {
            var allPackages = await GetAllPackagesAsync();

            var package = allPackages.FirstOrDefault(m =>
                       m.Package.CourseId.Equals(courseId, StringComparison.OrdinalIgnoreCase)) ??
                   throw new NullReferenceException(
                       $"Course with id '{courseId}' was not found in any of supported projects");

            return Config.Projects.First(m =>
                       m.ProjectId.Equals(package.ProjectId, StringComparison.OrdinalIgnoreCase)) ??
                   throw new NullReferenceException(
                       $"Project with id '{package.ProjectId}' was not found in configuration");
        }

        /// <summary>
        /// We need to filter out all objects that do not match our version because filtering does not work on 'modular_content' currently
        /// </summary>
        /// <returns></returns>
        public List<Page> FilterPagesToIncludeOnlyItemsWithVersion(List<Page> pages, string courseVersion)
        {
            return pages?.Where(page =>
            {
                page.Sections = page.Sections.Where(section =>
                {
                    section.Blocks = section.Blocks.Where(block =>
                    {
                        block.Components = block.Components
                            .Where(component => ContainsVersion(component, courseVersion)).ToList();
                        return true;
                    }).ToList();
                    return true;
                });
                return true;
            }).ToList();
        }

        /// <summary>
        /// Gets pages and nested course data structure from Kentico Cloud
        /// </summary>
        /// <param name="courseId">Course name as defined in KC</param>
        /// <param name="usePreview">Indicates is preview mode should be used</param>
        /// <returns></returns>
        public async Task<List<Page>> GetPagesAsync(string courseId, bool usePreview)
        {
            var project = await GetProjectForCourseAsync(courseId);
            var deliveryClient = GetDeliveryClient(project.ProjectId, usePreview, project.PreviewApiKey);

            var queryParams = new List<IQueryParameter>()
            {
                new EqualsFilter("system.type", Package.Codename),
                new EqualsFilter($"elements.{Package.CourseIdCodename}", courseId),
                new DepthParameter(Config.Depth)
            };

            var response = await deliveryClient.GetItemsAsync<Package>(queryParams);

            if (!response.Items.Any())
            {
                throw new NotSupportedException($"'{Package.Codename}' content type does not exist in project '{project.ProjectId}'. These should be exactly 1 item.");
            }

            if (response.Items.Count > 1)
            {
                throw new NotSupportedException($"'{Package.Codename}' content type '{project.ProjectId}' needs to have exactly 1 item. It currently has '{response.Items.Count}'");
            }

            var package = response.Items.First();

            if (!package.CourseVersionVersion.Any())
            {
                throw new NotSupportedException($"Course version is not set for course with id '{package.CourseId}' within project '{project.ProjectId}'");
            }

            if (package.CourseVersionVersion.Count() > 1)
            {
                throw new NotSupportedException($"Course with id '{package.CourseId}' may contain only 1 version of the course. It currently contains '{package.CourseVersionVersion.Count()}'");
            }

            var courseVersion = package.CourseVersionVersion.First().Codename;

            var pagesForGivenVersion = FilterPagesToIncludeOnlyItemsWithVersion(package.Pages.ToList(), courseVersion);

            return pagesForGivenVersion;
        }

        /// <summary>
        /// Gets all courses within a project
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public async Task<List<Package>> GetAllPackagesWithinProjectAsync(string projectId)
        {
            return (
                await GetDeliveryClient(projectId, false, null).GetItemsAsync<Package>()
            ).Items.ToList();
        }

        /// <summary>
        /// Constructs delivery client
        /// </summary>
        private IDeliveryClient GetDeliveryClient(string projectId, bool usePreview, string previewApiKey)
        {
            var client = DeliveryClientBuilder.WithOptions(
                    builder =>
                    {
                        var config = builder
                            .WithProjectId(projectId);

                        if (!usePreview)
                        {
                            // do not use preview
                            return
                                config.UseProductionApi.WaitForLoadingNewContent
                                .Build();
                        }

                        if (string.IsNullOrEmpty(previewApiKey))
                        {
                            throw new ArgumentNullException($"Preview api key is not set for project '{projectId}'");
                        }

                        // use preview
                        return
                            config.UsePreviewApi(previewApiKey).WaitForLoadingNewContent
                                .Build();
                    })
                .WithInlineContentItemsResolver(new DefaultContentItemResolver())
                .WithTypeProvider(new CustomTypeProvider())
                .Build();


            return client;
        }

        private bool ContainsVersion(object component, string version)
        {
            // we have to get version with reflection as DeliveryClient can't be used with interface/base/partial class
            var versionProperty = component.GetType().GetProperty(nameof(BaseComponent.CourseVersion));

            var versionValue = (IEnumerable<MultipleChoiceOption>) versionProperty.GetValue(component);

            return versionValue?.FirstOrDefault(m =>
                       m.Codename.Equals(version, StringComparison.OrdinalIgnoreCase)) != null;
        }

    }
}
