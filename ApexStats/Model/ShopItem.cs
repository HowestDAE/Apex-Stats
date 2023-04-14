using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApexStats.Model
{
    internal class ShopItem
    {
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "expireTimestamp")]
        public int ExpireTimestamp { get; set; }

        [JsonProperty(PropertyName = "originalPrice")]
        public int? OriginalPrice { get; set; }

        [JsonProperty(PropertyName = "pricing")]
        public List<Pricing> Pricing { get; set; }

        [JsonProperty(PropertyName = "asset")]
        public string ImageURL { get; set; }
    }

    public class Pricing
    {
        [JsonProperty(PropertyName = "ref")]
        public string Ref { get; set; }

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }
    }
}
