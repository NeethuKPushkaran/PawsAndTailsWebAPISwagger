using PawsAndTailsWebAPISwagger.DTOs;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItemDto>> GetAllCartItemsAsync();
        Task<CartItemDto> GetCartItemByIdAsync(int id);
        Task AddCartItemAsync(CartItemDto cartItemDto);
        Task UpdateCartItemAsync(CartItemDto cartItemDto);
        Task DeleteCartItemAsync(int id);
    }
}
