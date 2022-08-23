using ECommerceTest.CartApi.Repositories;
using ECommerceTest.Models;

using Microsoft.AspNetCore.Mvc;

using RabbitMQ.Client;

using System.Text;

namespace ECommerceTest.CartApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cart;
        private readonly IConfiguration _configuration;
        public CartController(ICartRepository cart, IConfiguration configuration)
        {
            _cart = cart;
            _configuration = configuration;
        }

        [Route("Add")]
        public async Task<bool> Add([FromBody] AddProductToCartDto newProduct)
        {
            //RabbitMQ dummy implementations
            string RabbitmqHost = _configuration.GetValue<string>("RabbitMQHost");
            var factory = new ConnectionFactory() { HostName = RabbitmqHost };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "locationSampleQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                //user ID from the claims
                int userID = 0;
                string message = $"Added To Cart=> ID:{newProduct.productID}, Quantity: {newProduct.quantity},UserID:{userID}";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "ProductAddQueue",
                                     basicProperties: null,
                                     body: body);
            }
            return await _cart.AddToCart(newProduct.productID, newProduct.quantity);
        }

    }
}