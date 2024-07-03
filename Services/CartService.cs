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

        public async Task<IEnumerable<CartDto>> GetAllCartsAsync()
        {
            try
            {
                var carts = await _cartRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<CartDto>>(carts);
            }

            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve all carts", ex);
            }
        }

        public async Task<CartDto> GetCartByIdAsync(int id)
        {
            try
            {
                var cart = await _cartRepository.GetByIdAsync(id);
                if(cart == null)
                {
                    throw new KeyNotFoundException($"Cart with ID {id} not found");
                }

                return _mapper.Map<CartDto>(cart);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to retrieve cart with ID {id}", ex);
            }
        }

        public async Task AddCartAsync(CreateCartDto cartDto)
        {
            try
            {
                if (cartDto == null) throw new ArgumentNullException(nameof(cartDto));


                var cart = _mapper.Map<Cart>(cartDto);
                await _cartRepository.AddAsync(cart);
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while adding to the cart: {ex.Message}");
            }
        }

        public async Task UpdateCartAsync(int id, UpdateCartDto cartDto)
        {
            try
            {
                var cart = await _cartRepository.GetByIdAsync(id);
                if(cart == null)
                {
                    throw new Exception("Cart not found.");
                }
                _mapper.Map(cartDto, cart);
                await _cartRepository.UpdateAsync(cart);
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to update cart", ex);
            }
        }

        public async Task DeleteCartAsync(int id)
        {
            try
            {
                var cart = await _cartRepository.GetByIdAsync(id);
                if (cart == null)
                {
                    throw new KeyNotFoundException($"Cart with ID {id} not found.");
                }
                await _cartRepository.DeleteAsync(cart);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to delete cart with ID {id}", ex);
            }
        }
       
        public async Task<CartDto> GetCartWithItemsAsync(int id)
        {
            try
            {
                var carts = await _cartRepository.GetCartWithItemsAsync(id);
                return _mapper.Map<CartDto>(carts);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to retrieve carts", ex);
            }
        }

    }
}
