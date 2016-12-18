namespace RoboBank.Customer.Service.Isolated.Models
{
    public class CustomerModel
    {
        public string Id { get; set; }

        public string ExternalId { get; set; }

        public string Person { get; set; }

        public ProfileModel Profile { get; set; }

        public bool AllowsSearch { get; set; }

        public bool CanHaveWebsite { get; set; }
    }
}
