using Newtonsoft.Json;

namespace Adapt.Model.Components
{
    public class GraphicComponentAdapt : BaseAdaptComponent
    {

        [JsonProperty("_graphic")]
        public FullGraphic Graphic { get; set; }

        public override AdaptComponentType Component => AdaptComponentType.Graphic;
    }
}
