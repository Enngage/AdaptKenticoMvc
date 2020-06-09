using System.Collections.Generic;
using Adapt.Model;
using CloudIntegration.Models;

namespace Adapt
{
    public interface IComponentService
    {
        List<BaseAdaptComponent> GetAllComponents(BlockAdapt parent, IEnumerable<IBaseComponent> inputComponents, bool throwExceptionForUnsupportedTypes);
    }
}
