namespace RoboBank.Customer.Domain
{
    public class Customer
    {
        public string Id { get; set; }

        public string ExternalId { get; set; }

        public PersonType Person { get; set; }

        public Profile Profile { get; set; }

        public bool AllowsSearch { get; set; }

        public bool CanHaveWebsite => Person == PersonType.Legal;
    }
}
