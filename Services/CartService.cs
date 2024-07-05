using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using PawsAndTailsWebAPISwagger.DTOs;
using PawsAndTailsWebAPISwagger.Interfaces;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public async Task<CartDto> GetCartByUserIdAsync(int userId)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                return _mapper.Map<CartDto>(cart);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to retrieve cart with User ID: {userId}", ex);
            }
        }

        public async Task<CartDto> GetCartByIdAsync (int id)
        {
            try
            {
                var cart = await _cartRepository.GetByIdAsync(id);
                return _mapper.Map<CartDto>(cart);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to retrieve cart with id: {id}", ex);
            }
        }
        public async Task<CartDto> AddToCartAsync(CartDto cartDto)
        {
            try
            {
                if (cartDto == null) throw new ArgumentNullException(nameof(cartDto));


                var cart = _mapper.Map<Cart>(cartDto);
                await _cartRepository.AddAsync(cart);
                return _mapper.Map<CartDto>(cart);
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while adding to the cart: {ex.Message}");
            }
        }

        //public async Task UpdateCartAsync(int cartId, CartDto cartDto)
        //{
        //    try
        //    {
        //        var cart = await _cartRepository.GetByIdAsync(cartId);
        //        if(cart == null)
        //        {
        //            throw new Exception("Cart not found.");
        //        }
        //        _mapper.Map(cartDto, cart);
        //        await _cartRepository.UpdateAsync(cart);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception("Failed to update cart", ex);
        //    }
        //}

        public async Task RemoveFromCartAsync(int cartId)
        {
            try
            {
                var cart = await _cartRepository.GetByIdAsync(cartId);
                if (cart == null)
                {
                    throw new KeyNotFoundException($"Cart with ID {cartId} not found.");
                }
                await _cartRepository.DeleteAsync(cart);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to delete cart with ID {cartId}", ex);
            }
        }

        public async Task DoCheckoutAsync(int userId)
        {
            try
            {

            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to DoCheckout for user ID: {userId}", ex);
            }
        }
    }
}
