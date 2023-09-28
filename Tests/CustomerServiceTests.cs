using Bogus;
using ProvaPub.Models;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests : FakeDbContext
    {
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            _customerService = new CustomerService(_ctx);
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task ShouldReturnExceptionIfCustomerIdIsInvalid(int invalidCustomerId)
        {
            // Arrange
            const decimal purchaseValue = 100m;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                await _customerService.CanPurchase(invalidCustomerId, purchaseValue);
            });
        }
        
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task ShouldReturnExceptionIfPurchaseValueIsInvalid(decimal invalidPurchaseValue)
        {
            // Arrange
            const int validCustomerId = 1;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            {
                await _customerService.CanPurchase(validCustomerId, invalidPurchaseValue);
            });
        }
        
       
        [Fact]
        public async Task ShouldReturnExceptionIfClientIsNonRegistered()
        {
            // Arrange
            const int customerId = 1;
            const decimal purchaseValue = 100m;

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await _customerService.CanPurchase(customerId, purchaseValue);
            });
        }
        
        [Fact]
        public async Task ShouldReturnFalseIfClientTriesToPurchaseMoreThanOnceInAMonth()
        {
            // Arrange
            const int customerId = 1;
            const decimal purchaseValue = 50m;

            _ctx.Customers.Add(new Customer { Id = customerId, Name = new Faker().Person.FullName});
            _ctx.Orders.Add(new Order { CustomerId = customerId, OrderDate = DateTime.UtcNow, Value = 100m});
            await _ctx.SaveChangesAsync();

            // Act
            var result = await _customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.False(result);
        }

        
        [Fact]
        public async Task ShouldReturnTrueIfNewClientTriesToPurchaseMoreThan100()
        {
            // Arrange
            const int customerId = 1;
            const decimal purchaseValue = 50m;

            _ctx.Customers.Add(new Customer { Id = customerId, Name = new Faker().Person.FullName});
            await _ctx.SaveChangesAsync();

            // Act
            var result = await _customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.True(result);
        }
        
        [Fact]
        public async Task ShouldReturnTrueIfClientTriesToPurchaseInNextMonth()
        {
            // Arrange
            const int customerId = 1;
            const decimal purchaseValue = 50m;

            _ctx.Customers.Add(new Customer { Id = customerId, Name = new Faker().Person.FullName});
            _ctx.Orders.Add(new Order { CustomerId = customerId, OrderDate = DateTime.UtcNow.AddMonths(-1), Value = 100m});
            await _ctx.SaveChangesAsync();

            // Act
            var result = await _customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.True(result);
        }
    }
}
