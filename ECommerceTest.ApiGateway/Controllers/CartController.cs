using ECommerceTest.ApiGateway.Services;
using ECommerceTest.Models;

using Microsoft.AspNetCore.Mvc;

namespace ECommerceTest.ApiGateway.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cart;

        public CartController(ICartService cart)
        {
            _cart = cart;
        }

        [HttpPost("AddToCart")]
        public async Task<bool> AddToCart([FromBody] Product product, int quantity = 1)
        {
            await _cart.AddToCart(product, quantity);
            return true;
        }
    }
}