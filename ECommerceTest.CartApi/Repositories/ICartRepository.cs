namespace ECommerceTest.CartApi.Repositories
{
    public interface ICartRepository
    {
        Task<bool> AddToCart(int productID, int quantity);
    }
}