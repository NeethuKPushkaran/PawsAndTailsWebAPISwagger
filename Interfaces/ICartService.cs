using PawsAndTailsWebAPISwagger.DTOs;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartService
    {
        //Task<IEnumerable<CartDto>> GetAllCartsAsync();
        //Task<CartDto> GetCartByIdAsync(int CartId);
        Task<CartDto> GetCartByUserIdAsync(int userId);
        Task AddToCartAsync(int userId, int productId, int quantity);
        //Task<CartDto> UpdateCartAsync(int cartId, CartDto cartDto);
        //Task RemoveFromCartAsync(int cartId, int productId);
        Task IncreaseQuantityAsync(int cartItemId);
        Task DecreaseQuantityAsync(int cartItemId);
        //Task DoCheckoutAsync(int cartId);
    }
}
