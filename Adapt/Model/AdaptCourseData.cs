using System.Collections.Generic;

namespace Adapt.Model
{
    public class AdaptCourseData
    {
        public List<PageAdapt> Pages { get; } 
        public List<ArticleAdapt> Articles { get; }
        public List<BlockAdapt> Blocks { get; } 
        public List<ComponentAdapt> Components { get; }

        public AdaptCourseData(List<PageAdapt> pages, List<ArticleAdapt> articles, List<BlockAdapt> blocks, List<ComponentAdapt> components)
        {
            Pages = pages;
            Articles = articles;
            Blocks = blocks;
            Components = components;
        }
      
    }
}
