using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RoboBank.Customer.Domain.Tests
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void CanHaveWebsite_ShouldReturnTrue_WhenPersonIsLegal()
        {
            // Arrange
            var customer = new Customer
            {
                Person = PersonType.Legal
            };

            // Act & Assert
            Assert.IsTrue(customer.CanHaveWebsite);
        }

        [TestMethod]
        public void CanHaveWebsite_ShouldReturnFalse_WhenPersonIsNatural()
        {
            // Arrange
            var customer = new Customer
            {
                Person = PersonType.Natural
            };

            // Act & Assert
            Assert.IsFalse(customer.CanHaveWebsite);
        }
    }
}
