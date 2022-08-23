using ECommerceTest.Models;

namespace ECommerceTest.ApiGateway.Services
{
    public interface ICartService
    {
        Task<bool> AddToCart(Product ProductToAdd, int quantity);
    }
}