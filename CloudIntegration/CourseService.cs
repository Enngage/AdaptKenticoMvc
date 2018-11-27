﻿using System;
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

            foreach (var projectId in Config.AllProjectIds)
            {
                var deliveryClient = GetDeliveryClient(projectId);

                packages.AddRange((await deliveryClient.GetItemsAsync<Package>()).Items.Select(m => new PackageDto()
                {
                    Package = m,
                    ProjectId = projectId
                }));
            }

            return packages;
        }

        public async Task<Package> GetPackageAsync(string projectId, string courseId)
        {
            return (
                    await GetDeliveryClient(projectId).GetItemsAsync<Package>(
                        new LimitParameter(1),
                        new EqualsFilter($"elements.{Package.CourseIdCodename}", courseId)
                    )
                ).Items
                .First();
        }

        public async Task<List<string>> GetPackageVersionsAsync(string projectId)
        {
            return (await GetDeliveryClient(projectId)
                    .GetContentElementAsync(Package.Codename, Package.CourseVersionVersionCodename))
                .Options.Select(m => m.Codename).ToList();
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
        /// <param name="projectId">ProjectId of course project</param>
        /// <param name="courseId">Course name as defined in KC</param>
        /// <returns></returns>
        public async Task<List<Page>> GetPagesAsync(string projectId, string courseId)
        {
            var deliveryClient = GetDeliveryClient(projectId);

            var queryParams = new List<IQueryParameter>()
            {
                new EqualsFilter("system.type", Package.Codename),
                new EqualsFilter($"elements.{Package.CourseIdCodename}", courseId),
                new DepthParameter(Config.Depth)
            };

            var response = await deliveryClient.GetItemsAsync<Package>(queryParams);

            if (!response.Items.Any())
            {
                throw new NotSupportedException($"'{Package.Codename}' content type does not exist in project '{projectId}'. These should be exactly 1 item.");
            }

            if (response.Items.Count > 1)
            {
                throw new NotSupportedException($"'{Package.Codename}' content type '{projectId}' needs to have exactly 1 item. It currently has '{response.Items.Count}'");
            }

            var package = response.Items.First();

            if (!package.CourseVersionVersion.Any())
            {
                throw new NotSupportedException($"Course version is not set for course with id '{package.CourseId}' within project '{projectId}'");
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
        /// <param name="projectId"></param>
        /// <returns></returns>
        public async Task<List<Package>> GetAllCoursesWithinProjectAsync(string projectId)
        {
            return (
                await GetDeliveryClient(projectId).GetItemsAsync<Package>()
            ).Items.ToList();
        }

        /// <summary>
        /// Constructs delivery client
        /// </summary>
        private IDeliveryClient GetDeliveryClient(string projectId)
        {
            #warning Enable inline content items after its been fixed (https://github.com/Kentico/delivery-sdk-net/issues/146)

            var client = DeliveryClientBuilder.WithOptions(
                    builder => builder.WithProjectId(projectId).UseProductionApi.WaitForLoadingNewContent.Build()
                    )
                //.WithInlineContentItemsResolver(new InlineCodeResolver())
                //.WithInlineContentItemsResolver(new InfoBoxResolver())
                .WithInlineContentItemsResolver(new DefaultContentItemResolver())
                .WithCodeFirstTypeProvider(new CustomTypeProvider())
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
