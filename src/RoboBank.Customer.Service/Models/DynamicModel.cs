using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RoboBank.Customer.Service.Models
{
    public class DynamicModel
    {
        [JsonExtensionData]
        public IDictionary<string, JToken> Properties { get; set; } = new Dictionary<string, JToken>();
    }
}
