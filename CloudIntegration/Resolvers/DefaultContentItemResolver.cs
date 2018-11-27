using System;
using System.Linq;
using CloudIntegration.Models.Cloud;
using KenticoCloud.Delivery.InlineContentItems;

namespace CloudIntegration.Resolvers
{
    public class DefaultContentItemResolver : IInlineContentItemsResolver<object>
    {

        public string Resolve(ResolvedContentItemData<object> data)
        {
            #warning This is a temporary fix due to bug where inlinte content resolvers do not work in Delivery sdk v8

            if (data.Item is InfoBox infoBox)
            {
                return $"<div>{infoBox.Content}</div>";
            }

            if (data.Item is CodeBlock codeBlock)
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
