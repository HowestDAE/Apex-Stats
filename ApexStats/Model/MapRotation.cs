using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStats.Model
{
    internal class MapRotation
    {
        [JsonProperty(PropertyName = "map")]
        public string Map { get; set; }
        [JsonProperty(PropertyName = "asset")]
        public string ImageURL { get; set; }
        [JsonProperty(PropertyName = "remainingSecs")]
        public int RemainingTime { get; set; }
    }
}
