using System.Linq;
using CloudIntegration.Models.Cloud;
using KenticoCloud.Delivery.InlineContentItems;

namespace CloudIntegration.Resolvers
{
    public class DefaultContentItemResolver : IInlineContentItemsResolver<object>
    {

        public string Resolve(ResolvedContentItemData<object> data)
        {
            if (data.Item is ResolvedContentItemData<object> itemWrapper)
            {
                if (itemWrapper.Item is InfoBox infoBox)
                {
                    return $"<div>{infoBox.Content}</div>";
                }

                if (itemWrapper.Item is CodeBlock codeBlock)
                {
                    return
                        $"<pre><code class=\"language-{codeBlock.AvailableLanguagesLanguage?.FirstOrDefault()?.Codename.ToLower().Trim()}\">" +
                            $"{System.Web.HttpUtility.HtmlEncode(codeBlock.Code)?.Trim()}" +
                        $"</code></pre>";
                }
            }

            return "Content not available.";
        }
    }
}
