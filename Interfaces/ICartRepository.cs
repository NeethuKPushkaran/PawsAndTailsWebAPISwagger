using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart> GetCartByUserIdAsync(int userId);
        //Task AddToCartAsync(int userId, int productId, int quantity);
        //Task RemoveFromCartAsync(int cartItemId);
        //Task UpdateCartItemQuantityAsync(int cartItemId, int quantity);
        //Task ClearCartAsync(int userId);
    }
}
