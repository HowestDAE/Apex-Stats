using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ApexStats.Model
{
    internal class PlayerStatistics
    {
        [JsonProperty(PropertyName = "platformInfo")]
        public PlatformInfo PlatformInfo { get; set; }
        [JsonProperty(PropertyName = "metadata")]
        public Metadata Metadata { get; set; }
        [JsonProperty(PropertyName = "segments")]
        public List<Segment> Segments { get; set; }
    }

    public class PlatformInfo
    {
        [JsonProperty(PropertyName = "platformSlug")]
        public string Platform { get; set; }
        [JsonProperty(PropertyName = "platformUserIdentifier")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "avatarUrl")]
        public string AvatarURL { get; set; }
    }

    public class Metadata
    {
        [JsonProperty(PropertyName = "currentSeason")]
        public int CurrentSeason { get; set; }
        [JsonProperty(PropertyName = "activeLegend")]
        public string ActiveLegend { get; set; }
        [JsonProperty(PropertyName = "activeLegendName")]
        public string ActiveLegendName { get; set; }
    }

    public class Segment
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "attributes")]
        public Attributes Attributes { get; set; }
        [JsonProperty(PropertyName = "metadata")]
        public SegmentMetadata Metadata { get; set; }
        [JsonProperty(PropertyName = "stats")]
       public Dictionary<string, Stat> Statistics { get; set; }
    }

    public class Attributes
    {
        public string id { get; set; }
    }

    public class SegmentMetadata
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageURL { get; set; }
        [JsonProperty(PropertyName = "bgImageUrl")]
        public string BgImageURL { get; set; }
        [JsonProperty(PropertyName = "portraitImageUrl")]
        public string PortraitImageURL { get; set; }
        [JsonProperty(PropertyName = "legendColor")]
        public string LegendColor { get; set; }
    }

    public class Stat
    {
        [JsonProperty(PropertyName = "percentile")]
        public float? Percentile { get; set; }
        [JsonProperty(PropertyName = "displayName")]
        public string DisplayName { get; set; }
        [JsonProperty(PropertyName = "metadata")]
        public StatMetadata Metadata { get; set; }
        [JsonProperty(PropertyName = "value")]
        public int Value { get; set; }
        [JsonProperty(PropertyName = "displayValue")]
        public string DisplayValue { get; set; }
    }

    public class StatMetadata
    {
        [JsonProperty(PropertyName = "iconUrl")]
        public string IconURL { get; set; }
        [JsonProperty(PropertyName = "rankName")]
        public string RankName { get; set; }
    }
}
