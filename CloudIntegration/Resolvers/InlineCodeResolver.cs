using System.Linq;
using CloudIntegration.Models.Cloud;
using KenticoCloud.Delivery.InlineContentItems;

namespace CloudIntegration.Resolvers
{

    public class InlineCodeResolver : IInlineContentItemsResolver<CodeBlock>
    {
        public string Resolve(ResolvedContentItemData<CodeBlock> data)
        {
            return
                $"<pre><code class=\"language-{data.Item.AvailableLanguagesLanguage?.FirstOrDefault()?.Codename.ToLower().Trim()}\">{data.Item.Code}</code></pre>";
        }
    }
}
