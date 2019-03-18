using System;
using System.Linq;
using CloudIntegration.Models.Cloud;
using KenticoCloud.Delivery.InlineContentItems;

namespace CloudIntegration.Resolvers
{
    public class DefaultContentItemResolver : IInlineContentItemsResolver<object>
    {

        public string Resolve(object data)
        {
            if (data is InfoBox infoBox)
            {
                var boxClass = "infobox";
                var infoBoxType = infoBox.Type.FirstOrDefault();
                if (infoBoxType != null)
                {

                    if (infoBoxType.Codename.Equals("note", StringComparison.OrdinalIgnoreCase))
                    {
                        boxClass += " note";
                    }

                    if (infoBoxType.Codename.Equals("idea", StringComparison.OrdinalIgnoreCase))
                    {
                        boxClass += " idea";
                    }

                    if (infoBoxType.Codename.Equals("warning", StringComparison.OrdinalIgnoreCase))
                    {
                        boxClass += " warning";
                    }
                }

                return $"<div class=\"{boxClass}\">{infoBox.Content}</div>";
            }

            if (data is CodeBlock codeBlock)
            {
                return
                    $"<pre><code class=\"language-{codeBlock.AvailableLanguagesLanguage?.FirstOrDefault()?.Codename.ToLower().Trim()}\">" +
                    $"{System.Web.HttpUtility.HtmlEncode(codeBlock.Code)?.Trim()}" +
                    $"</code></pre>";
            }

            return "Content not available.";
        }
    }
}
