using PawsAndTailsWebAPISwagger.DTOs;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartService
    {
        Task<CartDto> GetCartByIdAsync(int cartId);
        Task<CartDto> CreateCartAsync(CartDto cartDto);
        Task<CartDto>UpdateCartAsync(int cartId, CartDto cartDto);
        Task AddCartItemAsync(int cartId, CartItemDto cartItemDto);
        Task UpdateCartItemAsync(int cartItemId, CartItemDto cartItemDto);
        Task RemoveCartItemAsync(int cartItemId);
    }
}
