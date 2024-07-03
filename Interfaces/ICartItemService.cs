using PawsAndTailsWebAPISwagger.DTOs;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItemDto>> GetAllCartItemsAsync();
        Task<CartItemDto> GetCartItemByIdAsync(int cartItemId);
        Task AddCartItemAsync(CartItemDto cartItemDto);
        Task UpdateCartItemAsync(int cartItemid, CartItemDto cartItemDto);
        Task DeleteCartItemAsync(int cartItemid);
    }
}
