namespace RoboBank.Customer.Application.DTOs
{
    public class CustomerInfo
    {
        public string Id { get; set; }

        public string ExternalId { get; set; }

        public string Person { get; set; }

        public ProfileInfo Profile { get; set; }

        public bool AllowsSearch { get; set; }

        public bool CanHaveWebsite { get; set; }
    }
}
