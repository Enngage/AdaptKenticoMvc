using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudIntegration.Models;
using KenticoCloud.Delivery;

namespace CloudIntegration
{
    public interface IDataService
    {
        Task<IReadOnlyList<Article>> GetArticlesAsync(string projectId);
        Task<IReadOnlyList<Component>> GetComnponentsAsync(string projectId);
        Task<IReadOnlyList<Block>> GetBlocksAsync(string projectId);
        Task<IReadOnlyList<Page>> GetPagesAsync(string projectId);
    }
}
