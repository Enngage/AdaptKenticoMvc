using System.Collections.Generic;
using System.Linq;
using Adapt.Helpers;
using Adapt.Model.Types;
using CloudIntegration.Models;
using KenticoKontentModels;
using Newtonsoft.Json;

namespace Adapt.Model.Components
{
    public class NarrativeComponentAdapt : BaseAdaptComponent
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// Options are: setCompletionOn = inview | allItems
        /// </summary>
        [JsonProperty("_setCompletionOn")] public string SetCompletionOn { get; }

        public override AdaptComponentType Component => AdaptComponentType.Narrative;

        [JsonProperty("_hasNavigationInTextArea")]
        public bool HasNavigationInTextArea => false;

        [JsonProperty("mobileInstruction")]
        public string MobileInstruction => "Select the arrows followed by the plus icon to find out more.";

        [JsonProperty("_items")]
        public List<NarrativeComponentItem> Items { get; }

        public NarrativeComponentAdapt(string parentId, Narrative inputComponent) : base(parentId, inputComponent)
        {
            Body = inputComponent.Intro;
            SetCompletionOn = GetCompletion(YesOptionHelper.IsYesOptionChecked(inputComponent.RequireAllItemsBeSeen));
            Items = inputComponent?.NarrativeItems.Select(m => new NarrativeComponentItem()
            {
                Title = m.Title,
                Body = m.Text,
                Graphic = GraphicHelper.GetSimpleGraphic(m.Image)
            }).ToList();
        }

        public string GetCompletion(bool requireAllItemsToBeSeen)
        {
            return requireAllItemsToBeSeen ? "allItems" : "inview";
        }

        public class NarrativeComponentItem
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("body")]
            public string Body { get; set; }

            [JsonProperty("_graphic")]
            public SimpleGraphic Graphic { get; set; }

            [JsonProperty("strapline")] public string Strapline => Title;
        }
    }
}
