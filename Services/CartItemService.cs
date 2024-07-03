using AutoMapper;
using PawsAndTailsWebAPISwagger.DTOs;
using PawsAndTailsWebAPISwagger.Interfaces;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public CartItemService(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CartItemDto>> GetAllCartItemsAsync()
        {
            try
            {
                var cartItems = await _cartItemRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<CartItemDto>>(cartItems);
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException("CartItems not found.", ex);
            }
        }
        public async Task<CartItemDto> GetCartItemByIdAsync(int cartItemId)
        {
            try
            {
                var cartItems = await _cartItemRepository.GetByIdAsync(cartItemId);
                if (cartItems == null)
                {
                    throw new KeyNotFoundException($"Cart item with ID {cartItemId} not found");
                }
                return _mapper.Map<CartItemDto>(cartItems);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve cart item with ID {cartItemId}", ex);
            }
        }

        public async Task AddCartItemAsync(CartItemDto cartItemDto)
        {
            try
            {
                var cartItem = _mapper.Map<CartItem>(cartItemDto);
                await _cartItemRepository.AddAsync(cartItem);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add cart item", ex);
            }
        }

        public async Task UpdateCartItemAsync(int cartItemId, CartItemDto cartItemDto)
        {
            try
            {
                var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
                if (cartItem == null)
                {
                    throw new Exception("CartItem not found.");
                }

                _mapper.Map(cartItemDto, cartItem);
                await _cartItemRepository.UpdateAsync(cartItem);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update cart item", ex);
            }
        }

        public async Task DeleteCartItemAsync(int cartItemId)
        {
            try
            {
                var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
                if (cartItem == null)
                {
                    throw new KeyNotFoundException($"Cart item with ID {cartItemId} not found.");
                }
                await _cartItemRepository.DeleteAsync(cartItem);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to delete cart item with ID {cartItemId}", ex);
            }
        }

        //public async Task<IEnumerable<CartItemDto>> GetCartItemsByCartIdAsync(int cartId)
        //{
        //    try
        //    {
        //        var cartItems = await _cartItemRepository.GetCartItemsByCartIdAsync(cartId);
        //        return _mapper.Map<IEnumerable<CartItemDto>>(cartItems);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Failed to retrieve cart items for cart ID {cartId}", ex);
        //    }
        //}
    }
}
