using ECommerceTest.ApiGateway.Data;
using ECommerceTest.Models;

namespace ECommerceTest.ApiGateway.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CartService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> AddToCart(Product ProductToAdd, int quantity)
        {
            var client = _httpClientFactory.CreateClient(ApiClientNames.CartApi);

            var add = await client.PostAsJsonAsync("api/Cart/Add", new AddProductToCartDto(ProductToAdd.Id, quantity));
            return await add?.Content?.ReadFromJsonAsync<bool>();
        }
    }
}