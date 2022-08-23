using ECommerceTest.CartApi.Controllers;
using ECommerceTest.CartApi.Repositories;
using ECommerceTest.Models;

using ECommerceText.CartApi.DbContexts;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECommerceTest.CartApiTests.Controllers
{
    public class CartControllerTests
    {
        [Fact]
        public async Task Add_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IConfiguration config = new ConfigurationBuilder()
        .SetBasePath(Environment.ProcessPath)//path of current project
        .AddJsonFile("appsettings.json", optional: true)
        .AddEnvironmentVariables()
        .Build();

            var options = new DbContextOptionsBuilder<CartDatabaseContext>();
            options.UseSqlServer(config.GetValue<string>("CartDbLink"));

            var CartContext = new CartDatabaseContext(options.Options);
            ICartRepository cartRepository = new CartRepository(CartContext);
            AddProductToCartDto newProduct = new(1, 3);
            var cartController = new CartController(cartRepository, config);
            // Act
            var result = await cartController.Add(
                newProduct);

            // Assert
            Assert.True(false);
        }
    }
}