using CloudIntegration.Models;
using Newtonsoft.Json;

namespace Adapt.Model.Components
{
    public class TextComponentAdapt : BaseAdaptComponent
    {

        [JsonProperty("body")]
        public string Body { get; }

        public override AdaptComponentType Component => AdaptComponentType.Text;

        public TextComponentAdapt(string parentId, Text inputComponent) : base(parentId, inputComponent)
        {
            Body = inputComponent.Body;
        }
    }
}
