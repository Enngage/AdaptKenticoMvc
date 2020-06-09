using System.Collections.Generic;
using Adapt.Model;
using CloudIntegration.Models;
using KenticoKontentModels;

namespace Adapt
{
    public interface IAdaptService
    {
        AdaptCourseData GenerateCourseData(List<Page> inputPages, Package course);
        List<ArticleAdapt> GetArticles(PageAdapt parent, List<Section> inputArticles);
        List<BlockAdapt> GetBlocks(ArticleAdapt parent, List<Block> inputBlocks, ref int trackingId);
        List<BaseAdaptComponent> GetComponents(BlockAdapt parent, IEnumerable<IBaseComponent> inputComponents);
        List<PageAdapt> GetPages(List<Page> inputPages);
        AdaptCourseConfig GetCourseConfig(Package package);

    }
}