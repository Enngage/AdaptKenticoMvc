﻿using System;
using System.Collections.Generic;
using System.Linq;
using Adapt.Model;
using CloudIntegration.Models;

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
            var components = new List<ComponentAdapt>();

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
                Components = m.Components.ToList(),
                Id = m.System.Id,
                ParentId = parent.Id,
                Body = m.Body,
                Title = m.Title,
                DisplayTitle = m.DisplayTitle,
                TrackingId = m.System.Id
            }).ToList();
        }

        public List<ComponentAdapt> GetComponents(BlockAdapt parent, List<Component> inputComponents)
        {
            return inputComponents.Select(m => new ComponentAdapt()
            {
                Id = m.System.Id,
                ParentId = parent.Id,
                Body = m.Body,
                Title = m.Title,
                DisplayTitle = m.DisplayTitle,
                Component = AdaptComponentType.Text
                
            }).ToList();
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
                Graphics = null,
                Title = m.Title,
                DisplayTitle = m.DisplayTitle
            }).ToList();
        }


    }
}
