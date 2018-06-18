using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudIntegration.Models;
using KenticoCloud.Delivery;

namespace CloudIntegration
{
    public class DataService : IDataService
    {

        public async Task<IReadOnlyList<Article>> GetArticlesAsync(string projectId)
        {
            var deliveryClient = GetDeliveryClient(projectId);

            var articlesResponse = await deliveryClient.GetItemsAsync<Article>(new EqualsFilter("system.type", "article"));

            return articlesResponse.Items;
        }

        public Task<IReadOnlyList<Component>> GetComnponentsAsync(string projectId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Block>> GetBlocksAsync(string projectId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Page>> GetPagesAsync(string projectId)
        {
            throw new NotImplementedException();
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
