using PawsAndTailsWebAPISwagger.DTOs;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartByUserIdAsync(int userId);
        Task<CartDto> GetCartByIdAsync(int id);
        Task<CartDto> AddToCartAsync(int userId, CartItemDto cartItemDto);
        Task<bool> RemoveFromCartAsync(int userId, int productId);
        Task<bool> DoCheckoutAsync(int userId);
        Task<bool> IncreaseQuantityAsync(int userId, int productId);
        Task<bool> DecreaseQuantityAsync(int userId, int productId);
    }
}
