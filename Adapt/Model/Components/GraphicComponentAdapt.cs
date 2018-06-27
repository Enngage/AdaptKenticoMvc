using System.Linq;
using CloudIntegration.Models;
using Newtonsoft.Json;

namespace Adapt.Model.Components
{
    public class GraphicComponentAdapt : BaseAdaptComponent
    {

        [JsonProperty("_graphic")]
        public FullGraphic Graphic { get; }

        public override AdaptComponentType Component => AdaptComponentType.Graphic;

        public GraphicComponentAdapt(string parentId, Graphic inputComponent) : base(parentId, inputComponent)
        {
            Graphic = new FullGraphic()
            {
                Alt = inputComponent.Alt,
                LargeSrc = inputComponent.LargeImage.FirstOrDefault()?.Url,
                SmallSrc = inputComponent.SmallImage.FirstOrDefault()?.Url
            };
        }
    }
}
