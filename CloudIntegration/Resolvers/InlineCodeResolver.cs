using System;
using System.Collections.Generic;
using System.Text;
using CloudIntegration.Models;
using KenticoCloud.Delivery.InlineContentItems;

namespace CloudIntegration.Resolvers
{

    public class InlineCodeResolver : IInlineContentItemsResolver<CodeBlock>
    {
        public string Resolve(ResolvedContentItemData<CodeBlock> data)
        {
            return
                $"Should there be some specific HTML Aaron? Original code value: {data.Item.Code}";
        }
    }
}
