using PawsAndTailsWebAPISwagger.DTOs;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartService
    {
        Task<IEnumerable<CartDto>> GetAllCartsAsync();
        Task<CartDto> GetCartByIdAsync(int id);
        Task AddCartAsync(CreateCartDto cartDto);
        Task UpdateCartAsync(int id, UpdateCartDto cartDto);
        Task DeleteCartAsync(int id);
        Task<IEnumerable<CartDto>> GetCartWithItemsAsync(int id);
    }
}
