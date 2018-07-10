
using System;
using System.Collections.Generic;
using System.Linq;
using CloudIntegration.Models.Enums;
using KenticoCloud.Delivery;

namespace CloudIntegration.Models.Cloud
{
    public partial class InfoBox 
    {
        public const string Codename = "infobox";
        public const string ContentCodename = "content";
        public const string TypeCodename = "type";

        public ContentItemSystemAttributes System { get; set; }

        public string Content { get; set; }
        public IEnumerable<MultipleChoiceOption> Type { get; set; }

        public string CssClassName
        {
            get
            {
                if (InfoBoxType == InfoBoxTypeEnum.Unknown)
                {
                    return null;
                }

                var baseCssClass = "infobox";

                if (InfoBoxType == InfoBoxTypeEnum.Idea)
                {
                    return baseCssClass + " idea";
                }

                if (InfoBoxType == InfoBoxTypeEnum.Note)
                {
                    return baseCssClass + " note";
                }

                if (InfoBoxType == InfoBoxTypeEnum.Warning)
                {
                    return baseCssClass + " warning";
                }

                return null;
            }
        }

        public InfoBoxTypeEnum InfoBoxType
        {
            get
            {
                if (Type == null)
                {
                    return InfoBoxTypeEnum.Unknown;
                }

                if (!Type.Any())
                {
                    return InfoBoxTypeEnum.Unknown;
                }

                var optionCodename = Type.First();

                if (optionCodename.Codename.Equals("Note", StringComparison.OrdinalIgnoreCase))
                {
                    return InfoBoxTypeEnum.Note;
                }

                if (optionCodename.Codename.Equals("Idea", StringComparison.OrdinalIgnoreCase))
                {
                    return InfoBoxTypeEnum.Idea;
                }

                if (optionCodename.Codename.Equals("Warning", StringComparison.OrdinalIgnoreCase))
                {
                    return InfoBoxTypeEnum.Warning;
                }

                throw new NotSupportedException($"Unsupported option info box option '{optionCodename}'");
            }
        }
    }
}