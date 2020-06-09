
using System.Collections.Generic;
using System.Linq;
using Adapt.Helpers;
using Adapt.Model.Types;
using CloudIntegration.Models;
using Kentico.Kontent.Delivery.ContentTypes.Element;
using Newtonsoft.Json;

namespace Adapt.Model
{
    public abstract class BaseAdaptComponent : BaseAdaptModel
    {

        public override AdaptModelType Type { get; } = AdaptModelType.Component;

        [JsonProperty("instruction")]
        public string Instructions { get; set; }

        [JsonProperty("_layout")]
        public string Layout { get; set; }

        [JsonProperty("_component")] public string ComponentJson => Component.Type;

        [JsonProperty("_pageLevelProgress")]
        public PageLevelProgressAdapt PageLevelProgress { get; set; }

        [JsonProperty("_isOptional")]
        public bool IsOptional { get; set; }

        [JsonProperty("_classes")]
        public string Classes { get; set; }

        [JsonIgnore]
        public abstract AdaptComponentType Component { get; }

        protected BaseAdaptComponent(string parentId, IBaseComponent inputComponent)
        {
            Id = inputComponent.System.Id;
            ParentId = parentId;
            DisplayTitle = string.IsNullOrEmpty(inputComponent.DisplayTitle) ? inputComponent.Title : inputComponent.DisplayTitle; // user title if display title is not set
            Instructions = inputComponent.Instructions;
            Layout = GetLayout(inputComponent.Layout);
            Title = inputComponent.Title;
            IsOptional = YesOptionHelper.IsYesOptionChecked(inputComponent.IsOptional);
            Classes = string.Join(" ", inputComponent.ComponentClasses.Select(m => m.Name)); // take name because codename might ruin class name
            PageLevelProgress = new PageLevelProgressAdapt()
            {
                IsEnabled = YesOptionHelper.IsYesOptionChecked(inputComponent.IncludeInProgress)
            };
        }



        public string GetLayout(IEnumerable<MultipleChoiceOption> options)
        {
            // its a radio button and we are interested only in first value
            return options?.FirstOrDefault()?.Name?.ToLower();
        }
    }
}
