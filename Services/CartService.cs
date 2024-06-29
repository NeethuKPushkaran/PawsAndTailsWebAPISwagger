using AutoMapper;
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

        public async Task<CartDto> GetCartByIdAsync(int cartId)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> CreateCartAsync(CartDto cartDto)
        {
            var cart = _mapper.Map<Cart>(cartDto);
            cart.CreatedAt = DateTime.UtcNow;
            await _cartRepository.AddAsync(cart);
            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> UpdateCartAsync(int cartId, CartDto cartDto)
        {
            var existingCart = await _cartRepository.GetByIdAsync(cartId);
            if(existingCart == null)
            {
                throw new ArgumentException("Cart not found");
            }

            _mapper.Map(cartDto, existingCart);
            await _cartRepository.UpdateAsync(existingCart);
            return _mapper.Map<CartDto>(existingCart);
        }

        public async Task AddCartItemAsync (int cartId, CartItemDto cartItemDto)
        {
            var cartItem = _mapper.Map<CartItem>(cartItemDto);
            await _cartRepository.AddCartItemAsync(cartId, cartItem);
        }

        public async Task UpdateCartItemAsync(int cartItemId, CartItemDto cartItemDto)
        {
            var existingCartItem = await _cartRepository.GetByIdAsync(cartItemId);
            if(existingCartItem == null)
            {
                throw new ArgumentException("Cart Item not found.");
            }

            _mapper.Map(cartItemDto, existingCartItem);
            await _cartRepository.UpdateCartItemsAsync(existingCartItem);
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            var existingCartItem = await _cartRepository.GetByIdAsync(cartItemId);
            if(existingCartItem == null)
            {
                throw new ArgumentException("Cart item not found.");
            }

            await _cartRepository.RemoveCartItemAsync(cartItemId);
        }
    }
}
