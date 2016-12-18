using System.Collections.Generic;

namespace RoboBank.Customer.Application.DTOs
{
    public class DynamicInfo
    {
        public IDictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
    }
}
