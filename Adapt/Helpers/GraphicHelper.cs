using System.Collections.Generic;
using System.Linq;
using Adapt.Model;
using Kentico.Kontent.Delivery.Abstractions;

namespace Adapt.Helpers
{
    public static class GraphicHelper
    {
        public static SimpleGraphic GetSimpleGraphic(IEnumerable<IAsset> assets)
        {
            // take only one asset
            var asset = assets.FirstOrDefault();

            if (asset == null)
            {
                return null;
            }

            return new SimpleGraphic()
            {
                Alt = asset.Name,
                Src = asset.Url
            };
        }
    }
}
