// This code was generated by a kontent-generators-net tool 
// (see https://github.com/Kentico/kontent-generators-net).
// 
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated. 
// For further modifications of the class, create a separate file with the partial class.

using System;
using System.Collections.Generic;
using System.Linq;
using CloudIntegration.Models;
using Kentico.Kontent.Delivery.Abstractions;

namespace KenticoKontentModels
{
    public partial class Block
    {
        public const string Codename = "block";
        public const string BodyCodename = "body";
        public const string ComponentsCodename = "components";
        public const string CourseVersionVersionCodename = "course_version__version";
        public const string DisplayTitleCodename = "display_title";
        public const string TitleCodename = "title";

        public string Body { get; set; }
        public IEnumerable<object> Components { get; set; }

        public IEnumerable<IBaseComponent> MappedComponents => Components.Cast<IBaseComponent>();
        public IEnumerable<IMultipleChoiceOption> CourseVersionVersion { get; set; }
        public string DisplayTitle { get; set; }
        public IContentItemSystemAttributes System { get; set; }
        public string Title { get; set; }
    }
}