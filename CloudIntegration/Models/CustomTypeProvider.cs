using System;
using CloudIntegration.Models.Components;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models
{
    public class CustomTypeProvider : ICodeFirstTypeProvider
    {
        public Type GetType(string contentType)
        {
            switch (contentType)
            {
                case "article":
                    return typeof(Article);
                case "block":
                    return typeof(Block);
                case "course_metadata":
                    return typeof(CourseMetadata);
                case "page":
                    return typeof(Page);
                case "text_component":
                    return typeof(TextComponent);
                default:
                    return null;
            }
        }
    }
}