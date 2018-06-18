using System.Collections.Generic;
using Adapt.Model;
using CloudIntegration.Models;

namespace Adapt
{
    public interface IAdaptService
    {
        AdaptCourseData GenerateCourseData(List<Page> inputPages);
        List<ArticleAdapt> GetArticles(PageAdapt parent, List<Article> inputArticles);
        List<BlockAdapt> GetBlocks(ArticleAdapt parent, List<Block> inputBlocks);
        List<ComponentAdapt> GetComponents(BlockAdapt parent, List<Component> inputComponents);
        List<PageAdapt> GetPages(List<Page> inputPages);
    }
}