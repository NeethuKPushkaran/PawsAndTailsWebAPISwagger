using PawsAndTailsWebAPISwagger.DTOs;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartDto>> GetAllCartsAsync();
        Task<CartDto> GetCartByIdAsync(int CartId);
        Task<CartDto> GetCartByUserIdAsync(int userId);
        Task<CartDto> AddToCartAsync(int userId, CartItemDto cartItemDto);
        Task<CartDto> UpdateCartAsync(int cartId, CartDto cartDto);
        Task<bool> RemoveFromCartAsync(int userId, int productId);
        Task<bool> DoCheckoutAsync(int userId);
        Task<bool> IncreaseQuantityAsync(int userId, int productId);
        Task<bool> DecreaseQuantityAsync(int userId, int productId);
    }
}
