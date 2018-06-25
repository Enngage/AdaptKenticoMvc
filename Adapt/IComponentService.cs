using System.Collections.Generic;
using Adapt.Model;
using Adapt.Model.Components;
using CloudIntegration;
using CloudIntegration.Models;
using KenticoCloud.Delivery;

namespace Adapt
{
    public interface IComponentService
    {
        List<BaseAdaptComponent> GetAllComponents(BlockAdapt parent, List<IBaseComponent> inputComponents, bool throwExceptionForUnsupportedTypes);
        SimpleGraphic GetSimpleGraphic(IEnumerable<Asset> assets);
        TextComponentAdapt GetTextComponent(Text inputComponent);
    }
}
