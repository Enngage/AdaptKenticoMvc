using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudIntegration.Models;
using KenticoCloud.Delivery;

namespace CloudIntegration
{
    public class CourseService : ICourseService
    {
        private string MasterProjectId { get; }

        public CourseService(string masterProjectId)
        {
            MasterProjectId = masterProjectId;
        }

        public async Task<List<Course>> GetSupportedCoursesAsync()
        {
            var deliveryClient = GetDeliveryClient(MasterProjectId);

            return (await deliveryClient.GetItemsAsync<Course>(new EqualsFilter("system.type", Course.Codename)))
                .Items.ToList();
        }

        public async Task<bool> IsCourseSupported(string projectId)
        {
            return (await GetSupportedCoursesAsync()).FirstOrDefault(m => m.Projectid.Equals(projectId)) != null;
        }

        public async Task<List<Page>> GetPagesAsync(string projectId)
        {
            if (!(await IsCourseSupported(projectId)))
            {
                throw new NotSupportedException($"Course with project id '{projectId}' is not supported!");
            }

            var deliveryClient = GetDeliveryClient(projectId, true);

            var response = await deliveryClient.GetItemsAsync<Page>(
                new EqualsFilter("system.type", Page.Codename),
                new DepthParameter(5)
               );

            return response.Items.ToList();
        }

        public async Task<CourseMetadata> GetCourseMetadataAsync(string projectId)
        {
            if (!(await IsCourseSupported(projectId)))
            {
                throw new NotSupportedException($"Course with project id '{projectId}' is not supported!");
            }

            // there should be only single instance of course metadata per project. Thats why
            // we take only the first one.
            return (
                 await GetDeliveryClient(projectId).GetItemsAsync<CourseMetadata>(new LimitParameter(1))
                ).Items
                .First();
        }


        private IDeliveryClient GetDeliveryClient(string projectId, bool waitForLoadingNewContent = false)
        {
            return new DeliveryClient(new DeliveryOptions()
            {
                ProjectId = projectId,
                WaitForLoadingNewContent = waitForLoadingNewContent
            })
            {
                CodeFirstModelProvider = { TypeProvider = new CustomTypeProvider() }
            };
        }
    }
}
