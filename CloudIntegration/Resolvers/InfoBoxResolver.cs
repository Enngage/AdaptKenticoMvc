using System.Linq;
using CloudIntegration.Models.Cloud;
using KenticoCloud.Delivery.InlineContentItems;

namespace CloudIntegration.Resolvers
{

    public class InfoBoxResolver : IInlineContentItemsResolver<InfoBox>
    {
        public string Resolve(ResolvedContentItemData<InfoBox> data)
        {
            return $"<div class=\"{data.Item.CssClassName}\">{data.Item.Content}</div>";
        }
    }
}
