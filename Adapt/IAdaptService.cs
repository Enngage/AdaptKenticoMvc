using System.Collections.Generic;
using Adapt.Model;
using CloudIntegration;
using CloudIntegration.Models;
using CloudIntegration.Models.Cloud;

namespace Adapt
{
    public interface IAdaptService
    {
        AdaptCourseData GenerateCourseData(List<Page> inputPages, Package course);
        List<ArticleAdapt> GetArticles(PageAdapt parent, List<Section> inputArticles);
        List<BlockAdapt> GetBlocks(ArticleAdapt parent, List<Block> inputBlocks);
        List<BaseAdaptComponent> GetComponents(BlockAdapt parent, List<IBaseComponent> inputComponents);
        List<PageAdapt> GetPages(List<Page> inputPages);
        AdaptCourseConfig GetCourseConfig(Package package);

    }
}