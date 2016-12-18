using System.Collections.Generic;

namespace RoboBank.Customer.Domain
{
    public class Dynamic
    {
        public IDictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();
    }
}