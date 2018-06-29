using System.Collections.Generic;

namespace Adapt.Model
{
    public class AdaptCourseData
    {
        public List<PageAdapt> Pages { get; } 
        public List<ArticleAdapt> Articles { get; }
        public List<BlockAdapt> Blocks { get; } 
        public List<BaseAdaptComponent> Components { get; }
        public AdaptCourseConfig Course { get; }

        public AdaptCourseData(List<PageAdapt> pages, List<ArticleAdapt> articles, List<BlockAdapt> blocks, List<BaseAdaptComponent> components, AdaptCourseConfig course)
        {
            Pages = pages;
            Articles = articles;
            Blocks = blocks;
            Components = components;
            Course = course;
        }
      
    }
}
