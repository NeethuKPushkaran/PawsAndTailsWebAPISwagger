using PawsAndTailsWebAPISwagger.DTOs;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartDto>> GetAllCartsAsync();
        Task<CartDto> GetCartByIdAsync(int id);
        Task AddCartAsync(CartDto cartDto);
        Task UpdateCartAsync(CartDto cartDto);
        Task DeleteCartAsync(int id);
        Task<IEnumerable<CartDto>> GetCartsByUserIdAsync(int userId);
    }
}
