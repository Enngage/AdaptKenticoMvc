using System.Collections.Generic;
using System.Linq;
using Adapt.Helpers;
using Adapt.Model.Types;
using CloudIntegration.Models;
using CloudIntegration.Models.Cloud;
using Newtonsoft.Json;

namespace Adapt.Model.Components
{
    public class NarrativeWithCodeComponentAdapt : BaseAdaptComponent
    {
        [JsonProperty("body")]
        public string Body { get; set; }

        /// <summary>
        /// Options are: setCompletionOn = inview | allItems
        /// </summary>
        [JsonProperty("_setCompletionOn")] public string SetCompletionOn { get; }

        public override AdaptComponentType Component => AdaptComponentType.NarrativeWithCode;

        [JsonProperty("_hasNavigationInTextArea")]
        public bool HasNavigationInTextArea => false;

        [JsonProperty("mobileInstruction")]
        public string MobileInstruction => "Select the arrows followed by the plus icon to find out more.";

        [JsonProperty("_items")]
        public List<NarrativeWithCodeComponentItem> Items { get; }

        public NarrativeWithCodeComponentAdapt(string parentId, NarrativeCode inputComponent) : base(parentId, inputComponent)
        {
            Body = inputComponent.Intro;
            SetCompletionOn = GetCompletion(YesOptionHelper.IsYesOptionChecked(inputComponent.RequireAllCodeBeSee));
            Items = inputComponent?.NarrativeCodeItems.Select(m => new NarrativeWithCodeComponentItem()
            {
                Title = m.Title,
                Body = m.Text,
                Code = new ComponentAdaptCode()
                {
                    Code = m.Code,
                    Lang = m.AvailableLanguagesLanguage?.FirstOrDefault()?.Codename?.ToLower()
                }
            }).ToList();
        }

        public string GetCompletion(bool requireAllItemsToBeSeen)
        {
            return requireAllItemsToBeSeen ? "allItems" : "inview";
        }

        public class NarrativeWithCodeComponentItem
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("body")]
            public string Body { get; set; }

            [JsonProperty("code")]
            public ComponentAdaptCode Code { get; set; }

            [JsonProperty("strapline")] public string Strapline => Title;
        }
    }
}
