using System.Collections.Generic;
using Adapt.Model;
using Adapt.Model.Components;
using CloudIntegration;
using CloudIntegration.Models;

namespace Adapt
{
    public interface IComponentService
    {
        List<BaseAdaptComponent> GetAllComponents(BlockAdapt parent, List<IBaseComponent> inputComponents, bool throwExceptionForUnsupportedTypes);

        TextComponentAdapt GetTextComponent(Text inputComponent);
    }
}
