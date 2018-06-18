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

        public async Task<List<Page>> GetPagesAsync(string projectId)
        {
            var deliveryClient = GetDeliveryClient(projectId);

            var articlesResponse = await deliveryClient.GetItemsAsync<Page>(
                new EqualsFilter("system.type", Page.Codename),
                new DepthParameter(5)
               );

            return articlesResponse.Items.ToList();
        }

        public async Task<CourseMetadata> GetCourseMetadataAsync(string projectId)
        {
            // there should be only single instance of course metadata per project. Thats why
            // we take only the first one.
            return (
                 await GetDeliveryClient(projectId).GetItemsAsync<CourseMetadata>(new LimitParameter(1))
                ).Items
                .First();
        }


        private IDeliveryClient GetDeliveryClient(string projectId)
        {
            return new DeliveryClient(new DeliveryOptions()
            {
                ProjectId = projectId,
            })
            {
                CodeFirstModelProvider = { TypeProvider = new CustomTypeProvider() }
            };
        }
    }
}
