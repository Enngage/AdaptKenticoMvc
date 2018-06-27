using System.Collections.Generic;
using System.Linq;
using Adapt.Helpers;
using Adapt.Model;
using CloudIntegration;
using CloudIntegration.Models;

namespace Adapt
{
    public class AdaptService : IAdaptService
    {
        /// <summary>
        /// This is id of the 'contentObject' item that is required by Adapt (by default)
        /// </summary>
        private const string DefaultPageParentId = "course";

        private IComponentService ComponentService { get; }

        public AdaptService(IComponentService componentService)
        {
            ComponentService = componentService;
        }

        /// <summary>
        /// Adapt will fail with NO ERROR if there is a parent with no child items (i.e. article without blocks, block without components...)
        /// Make sure that such parents are not added to final result
        /// </summary>
        public AdaptCourseData GenerateCourseData(List<Page> inputPages)
        {
            var pages = new List<PageAdapt>();
            var articles = new List<ArticleAdapt>();
            var blocks = new List<BlockAdapt>();
            var components = new List<BaseAdaptComponent>();

            foreach (var page in GetPages(inputPages))
            {
                var pageArticles = new List<ArticleAdapt>();

                foreach (var pageArticle in GetArticles(page, page.Articles))
                {
                    var articleBlocks = new List<BlockAdapt>();

                    foreach (var articleBlock in GetBlocks(pageArticle, pageArticle.Blocks))
                    {
                        // generate block components
                        var blockComponents = GetComponents(articleBlock, articleBlock.Components);
                    
                        // only add blocks with components
                        if (blockComponents.Any())
                        {
                            components.AddRange(blockComponents);
                            articleBlocks.Add(articleBlock);
                        }
                    }

                    if (articleBlocks.Any())
                    {
                        blocks.AddRange(articleBlocks);
                        pageArticles.Add(pageArticle);
                    }
                }

                if (pageArticles.Any())
                {
                    articles.AddRange(pageArticles);
                    pages.Add(page);
                }
            }

            return new AdaptCourseData(pages, articles, blocks, components);
        }


        public List<ArticleAdapt> GetArticles(PageAdapt parent, List<Section> inputArticles)
        {
            return inputArticles.Select(m => new ArticleAdapt()
            {
                Blocks = m.Blocks.ToList(),
                Body = m.Body,
                Id = m.System.Id,
                ParentId = parent.Id,
                Title = m.Title,
                DisplayTitle = m.DisplayTitle
            }).ToList();
        }

        public List<BlockAdapt> GetBlocks(ArticleAdapt parent, List<Block> inputBlocks)
        {
            return inputBlocks.Select(m => new BlockAdapt()
            {
                Components = m.Components.Cast<IBaseComponent>().ToList(),
                Id = m.System.Id,
                ParentId = parent.Id,
                Body = m.Body,
                Title = m.Title,
                DisplayTitle = m.DisplayTitle,
                TrackingId = m.System.Id
            }).ToList();
        }

        public List<BaseAdaptComponent> GetComponents(BlockAdapt parent, List<IBaseComponent> inputComponents)
        {
            return ComponentService.GetAllComponents(parent, inputComponents, false);
        }

        public List<PageAdapt> GetPages(List<Page> inputPages)
        {
            return inputPages.Select(m => new PageAdapt()
            {
                Articles = m.Sections.ToList(),
                Id = m.System.Id,
                ParentId = DefaultPageParentId,
                Body = m.Text,
                Duration = m.Duration,
                Instructions = m.Instructions,
                LinkText = m.LinkText,
                PageBody = m.Text,
                Graphic = GraphicsHelper.GetSimpleGraphic(m.Image),
                Title = m.Title,
                DisplayTitle = m.DisplayTitle
            }).ToList();
        }

    }
}
