using System.Collections.Generic;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models.Cloud
{
    public partial class Video : BaseComponent
    {
        public const string Codename = "video";
        public const string SetCompletionOnCodename = "set_completion_on";
        public const string VideoFileCodename = "videofile";
        public const string DescriptionCodename = "description";
        public const string TranscriptCodename = "transcript";
        public const string LoadingImageCodename = "loading_image";

        public IEnumerable<MultipleChoiceOption> SetCompletionOn { get; set; }
        public IEnumerable<Asset> VideoFile { get; set; }
        public string Description { get; set; }
        public string Transcript { get; set; }
        public IEnumerable<TaxonomyTerm> BasecomponentCompontentClasses { get; set; }
        public IEnumerable<Asset> LoadingImage { get; set; }
    }
}