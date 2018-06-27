using System.Collections.Generic;
using Adapt.Model;
using CloudIntegration;

namespace Adapt
{
    public interface IComponentService
    {
        List<BaseAdaptComponent> GetAllComponents(BlockAdapt parent, List<IBaseComponent> inputComponents, bool throwExceptionForUnsupportedTypes);
    }
}
