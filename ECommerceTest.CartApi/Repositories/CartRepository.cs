using ECommerceText.CartApi.DbContexts;

using Microsoft.EntityFrameworkCore;

namespace ECommerceTest.CartApi.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDatabaseContext _cartContext;

        public CartRepository(CartDatabaseContext cartContext)
        {
            _cartContext = cartContext;
        }

        public async Task<bool> AddToCart(int productID, int quantity)
        {
            //The dbcontext can be filterred with the user with identity claims, logically not implemented here
            var currentUserID = 0;//id taken as 0 for example
            var cart = await _cartContext.Cart.Where(x => x.UserId == currentUserID).Include(x => x.CartItems).FirstOrDefaultAsync();
            if (cart is not null)
            {
                cart.CartItems.Add(new()
                {
                    ProductId = productID,
                    Quantity = quantity,
                });
                await _cartContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}