﻿using System.Collections.Generic;
using Adapt.Model.Types;
using KenticoKontentModels;
using Newtonsoft.Json;

namespace Adapt.Model
{
    public class ArticleAdapt : BaseAdaptModel
    {
        [JsonIgnore]
        public List<Block> Blocks { get; set; }

        public override AdaptModelType Type { get; } = AdaptModelType.Article;

        [JsonProperty("body")]
        public string Body { get; set; }

    }
}
