using System;
using System.Collections.Generic;
using System.Linq;
using Adapt.Model;
using Adapt.Model.Components;
using AngleSharp.Dom.Css;
using CloudIntegration.Models;
using CloudIntegration.Models.Components;
using KenticoCloud.Delivery;

namespace Adapt
{
    public class AdaptService : IAdaptService
    {
        /// <summary>
        /// This is id of the 'contentObject' item that is required by Adapt (by default)
        /// </summary>
        private const string DefaultPageParentId = "course";

        public AdaptCourseData GenerateCourseData(List<Page> inputPages)
        {
            var pages = GetPages(inputPages);
            var articles = new List<ArticleAdapt>();
            var blocks = new List<BlockAdapt>();
            var components = new List<BaseAdaptComponent>();

            foreach (var page in pages)
            {
                // generate page articles
                var pageArticles = GetArticles(page, page.Articles);
                articles.AddRange(pageArticles);

                foreach (var pageArticle in pageArticles)
                {
                    // generate article blocks
                    var articleBlocks = GetBlocks(pageArticle, pageArticle.Blocks);
                    blocks.AddRange(articleBlocks);

                    foreach (var articleBlock in articleBlocks)
                    {
                        // generate block components
                        var blockComponents = GetComponents(articleBlock, articleBlock.Components);
                        components.AddRange(blockComponents);
                    }
                }
            }

            return new AdaptCourseData(pages, articles, blocks, components);
        }

        public List<ArticleAdapt> GetArticles(PageAdapt parent, List<Article> inputArticles)
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
            var components = new List<BaseAdaptComponent>();

            foreach (var inputComponent in inputComponents)
            {
                if (inputComponent is TextComponent textComponent)
                {
                    components.Add(new TextComponentAdapt()
                    {
                        Id = textComponent.System.Id,
                        ParentId = parent.Id,
                        Title = textComponent.BasecomponentTitle,
                        DisplayTitle = textComponent.BasecomponentDisplayTitle,
                        Body = textComponent.Body,
                        Instructions = textComponent.BasecomponentInstructions,
                        Layout = GetLayout(textComponent.BasecomponentLayout)
                    });
                }
                else if (inputComponent is GraphicComponent graphicComponent)
                {
                    components.Add(new GraphicComponentAdapt()
                    {
                        Id = graphicComponent.System.Id,
                        ParentId = parent.Id,
                        Title = graphicComponent.BasecomponentTitle,
                        DisplayTitle = graphicComponent.BasecomponentDisplayTitle,
                        Instructions = graphicComponent.BasecomponentInstructions,
                        Layout = GetLayout(graphicComponent.BasecomponentLayout),
                        Graphic = new FullGraphic()
                        {
                            Alt = graphicComponent.Alt,
                            LargeSrc = graphicComponent.LargeImage.FirstOrDefault()?.Url,
                            SmallSrc = graphicComponent.SmallImage.FirstOrDefault()?.Url
                        }
                    });
                }
                else
                {
                    throw new NotSupportedException($"Unsupported component type '{inputComponent.GetType().Name}' for component '{inputComponent.System.Codename}'");
                }
            }
            return components;
    }

        public List<PageAdapt> GetPages(List<Page> inputPages)
        {
            return inputPages.Select(m => new PageAdapt()
            {
                Articles = m.Articles.ToList(),
                Id = m.System.Id,
                ParentId = DefaultPageParentId,
                Body = m.Text,
                Duration = m.Duration,
                Instructions = m.Instructions,
                LinkText = m.LinkText,
                PageBody = m.Text,
                Graphic = GetGraphics(m.Image),
                Title = m.Title,
                DisplayTitle = m.DisplayTitle
            }).ToList();
        }

        private string GetLayout(IEnumerable<MultipleChoiceOption> options)
        {
            // its a radio button and we are interested only in first value
            return options?.FirstOrDefault()?.Name?.ToLower();
        }

        private SimpleGraphic GetGraphics(IEnumerable<Asset> assets)
        {
            // take only one asset
            var asset = assets.FirstOrDefault();

            if (asset == null)
            {
                return null;
            }

            return new SimpleGraphic()
            {
                Alt = asset.Name,
                Src = asset.Url
            };
        }
    }
}
