using System.Collections.Generic;
using Adapt.Model.Components;

namespace Adapt.Model
{
    public class AdaptCourseData
    {
        public List<PageAdapt> Pages { get; } 
        public List<ArticleAdapt> Articles { get; }
        public List<BlockAdapt> Blocks { get; } 
        public List<BaseAdaptComponent> Components { get; }

        public AdaptCourseData(List<PageAdapt> pages, List<ArticleAdapt> articles, List<BlockAdapt> blocks, List<BaseAdaptComponent> components)
        {
            Pages = pages;
            Articles = articles;
            Blocks = blocks;
            Components = components;
        }
      
    }
}
