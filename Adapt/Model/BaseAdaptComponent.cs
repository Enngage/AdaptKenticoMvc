
using System;
using System.Collections.Generic;
using System.Linq;
using CloudIntegration;
using KenticoCloud.Delivery;
using Newtonsoft.Json;

namespace Adapt.Model
{
    public abstract class BaseAdaptComponent : BaseAdaptModel
    {
        public const string IsRequiredYesOption = "yes";

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
            DisplayTitle = inputComponent.DisplayTitle;
            Instructions = inputComponent.Instructions;
            Layout = GetLayout(inputComponent.Layout);
            Title = inputComponent.Title;
            IsOptional = IsYesOptionChecked(inputComponent.IsOptional);
            Classes = string.Join(" ", inputComponent.ComponentClasses.Select(m => m.Name)); // take name because codename might ruin class name
            PageLevelProgress = new PageLevelProgressAdapt()
            {
                IsEnabled = IsYesOptionChecked(inputComponent.IncludeInProgress)
            };
        }

        public bool IsYesOptionChecked(IEnumerable<MultipleChoiceOption> options)
        {
            return options?.FirstOrDefault()?.Codename.Equals(IsRequiredYesOption, StringComparison.OrdinalIgnoreCase) ?? false;
        }

        public string GetLayout(IEnumerable<MultipleChoiceOption> options)
        {
            // its a radio button and we are interested only in first value
            return options?.FirstOrDefault()?.Name?.ToLower();
        }
    }
}
