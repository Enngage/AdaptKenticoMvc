using Adapt.Model.Types;
using CloudIntegration.Models.Cloud;
using Newtonsoft.Json;

namespace Adapt.Model.Components
{
    public class TextWithCodeComponentAdapt : BaseAdaptComponent
    {

        [JsonProperty("body")]
        public string Body { get; }

        public override AdaptComponentType Component => AdaptComponentType.TextWithCode;

        public TextWithCodeComponentAdapt(string parentId, Text inputComponent) : base(parentId, inputComponent)
        {
            Body = inputComponent.Body;
        }
    }
}
