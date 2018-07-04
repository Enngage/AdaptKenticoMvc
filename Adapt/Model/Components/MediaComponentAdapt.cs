using System.Collections.Generic;
using System.Linq;
using Adapt.Model.Types;
using CloudIntegration.Models;
using CloudIntegration.Models.Cloud;
using Newtonsoft.Json;

namespace Adapt.Model.Components
{
    public class MediaComponentAdapt : BaseAdaptComponent
    {
        /// <summary>
        /// Options: _setCompletionOn = inview | play | ended"
        /// </summary>
        [JsonProperty("_setCompletionOn")]
        public string SetComplitionOn => "play";

        [JsonProperty("_useClosedCaptions")]
        public bool UseClosedCaptions => true;

        [JsonProperty("_allowFullScreen")]
        public bool AllowFullScreen => true;

        [JsonProperty("_media")]
        public MediaComponentMedia Media { get;}

        [JsonProperty("_playerOptions")]
        public MediaComponentAdaptPlayerOptions PlayerOptions => new MediaComponentAdaptPlayerOptions();

        public override AdaptComponentType Component => AdaptComponentType.Media;

        public MediaComponentAdapt(string parentId, Video video): base(parentId, video)
        {
           Media = new MediaComponentMedia()
           {
               Mp4 = video.VideoFile?.FirstOrDefault()?.Url,
               Poster = video.LoadingImage?.FirstOrDefault()?.Url,
               Cc = new MediaComponentMediaCc()
               {
               }
           };
        }

        public class MediaComponentMedia
        {
            [JsonProperty("mp4")]
            public string Mp4 { get; set; }

            [JsonProperty("poster")]
            public string Poster { get; set; }

            [JsonProperty("cc")]
            public MediaComponentMediaCc Cc { get; set; }
        }

        public class MediaComponentMediaCc
        {
            [JsonProperty("srclang")] public string SrcLang => "en";

            [JsonProperty("src")]
            public string Src { get; set; }
        }

        public class MediaComponentAdaptPlayerOptions
        {
            [JsonProperty("features")]
            public List<string> Features => new List<string>()
            {
                "playpause",
                "progress",
                "current",
                "duration",
                "speed",
                "volume",
                "fullscreen"
            };
        }
    }

}
