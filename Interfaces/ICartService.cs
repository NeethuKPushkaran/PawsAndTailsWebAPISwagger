using PawsAndTailsWebAPISwagger.DTOs;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartByUserIdAsync(int userId);
        Task<CartDto> GetCartByIdAsync(int userId);
        Task AddCartAsync(CartDto cartDto);
        Task UpdateCartAsync(int cartId, CartDto cartDto);
        Task DeleteCartAsync(int cartId);
    }
}
