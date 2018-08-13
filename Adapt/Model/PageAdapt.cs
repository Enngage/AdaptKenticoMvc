﻿using System.Collections.Generic;
using Adapt.Model.Types;
using CloudIntegration.Models;
using CloudIntegration.Models.Cloud;
using Newtonsoft.Json;

namespace Adapt.Model
{
    public class PageAdapt : BaseAdaptModel
    {
        [JsonIgnore]
        public List<Section> Articles { get; set; }

        [JsonProperty("body")]
        public string Body { get; set; }

        [JsonProperty("pageBody")]
        public string PageBody { get; set; }

        [JsonProperty("instructions")]
        public string Instructions { get; set; }

        [JsonProperty("duration")]
        public decimal? Duration { get; set; }

        [JsonProperty("linkText")]
        public string LinkText { get; set; }

        [JsonProperty("_graphic")]
        public SimpleGraphic Graphic { get; set; }

        [JsonProperty("_pageLevelProgress")]
        public PageLevelProgressAdapt PageLevelProgress { get; set; } = new PageLevelProgressAdapt()
        {
            IsEnabled = true
        };

        public override AdaptModelType Type { get; } = AdaptModelType.Page;
    }
}
