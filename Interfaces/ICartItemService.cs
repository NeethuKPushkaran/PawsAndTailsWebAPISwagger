using PawsAndTailsWebAPISwagger.DTOs;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItemDto>> GetItemsByCartIdAsync(int cardId);
        //Task<CartItemDto> GetItemsByCartIdAsync(int cardId);
        Task AddCartItemAsync(CreateCartItemDto cartItemDto);
        Task UpdateCartItemAsync(int id, UpdateCartItemDto cartItemDto);
        Task DeleteCartItemAsync(int id);
    }
}
