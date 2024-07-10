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
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IMapper mapper, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
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
                throw new Exception($"Failed to retrieve All Carts", ex);
            }
        }

        public async Task<CartDto> GetCartByIdAsync(int CartId)
        {
            try
            {
                var cart = await _cartRepository.GetByIdAsync(CartId);
                if (cart == null) throw new Exception("Cart not found");
                return _mapper.Map<CartDto>(cart);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve cart by id", ex);
            }
        }
        public async Task<CartDto> GetCartByUserIdAsync(int userId)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null) throw new Exception("Cart not found");
                return _mapper.Map<CartDto>(cart); ;
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to retrieve cart with User ID: {userId}", ex);
            }
        }

        public async Task<CartDto> AddToCartAsync(int userId, CartItemDto cartItemDto)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null)
                {
                    cart = new Cart { UserId = userId, CreatedAt = DateTime.UtcNow };
                    var product = await _productRepository.GetByIdAsync(cartItemDto.ProductId);
                    var cartItem = _mapper.Map<CartItem>(cartItemDto);
                    cartItem.Price = product.OurPrice * cartItemDto.Quantity;
                    cart.CartItems.Add(cartItem);
                    await _cartRepository.AddAsync(cart);
                }
                else
                {
                    var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == cartItemDto.ProductId);
                    if (existingItem == null)
                    {
                        var product = await _productRepository.GetByIdAsync(cartItemDto.ProductId);
                        var cartItem = _mapper.Map<CartItem>(cartItemDto);
                        cartItem.Price = product.OurPrice * cartItemDto.Quantity;
                        cart.CartItems.Add(cartItem);
                    }
                    else
                    {
                        existingItem.Quantity += cartItemDto.Quantity;
                        existingItem.Price += existingItem.Product.OurPrice * cartItemDto.Quantity;
                    }
                    await _cartRepository.UpdateAsync(cart);
                }

                cart.TotalPrice = cart.CartItems.Sum(ci => ci.Price);
                await _cartRepository.UpdateAsync(cart);

                return _mapper.Map<CartDto>(cart);
            }
            catch(Exception ex)
            {
                throw new Exception($"An error occurred while adding to the cart: {ex.Message}");
            }
        }

        public async Task<CartDto> UpdateCartAsync(int cartId, CartDto cartDto)
        {
            try
            {
                var cart = await _cartRepository.GetByIdAsync(cartId);
                if (cart == null) throw new Exception("Cart not found");

                cart.UserId = cartDto.UserId;
                cart.CreatedAt = cartDto.CreatedAt;

                foreach (var cartItemDto in cartDto.CartItems)
                {
                    var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemDto.CartItemId);
                    if (cartItem == null)
                    {
                        cartItem = _mapper.Map<CartItem>(cartItemDto);
                        cart.CartItems.Add(cartItem);
                    }
                    else
                    {
                        _mapper.Map(cartItemDto, cartItem);
                    }
                }

                cart.TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Price);

                await _cartRepository.UpdateAsync(cart);

                return _mapper.Map<CartDto>(cart);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update cart", ex);
            }
        }

        public async Task<bool> RemoveFromCartAsync(int userId, int productId)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null) throw new Exception("Cart not found");

                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (cartItem == null) throw new Exception("CartItem not found");

                cart.CartItems.Remove(cartItem);
                cart.TotalPrice = cart.CartItems.Sum(ci => ci.Price);
                await _cartRepository.UpdateAsync(cart);

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to delete user with ID {userId}", ex);
            }
        }

        public async Task<bool> DoCheckoutAsync(int userId)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null) throw new Exception("Cart not found");

                cart.CartItems.Clear();
                cart.TotalPrice = 0;
                await _cartRepository.UpdateAsync(cart);

                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"failed to docheckout for user id: {userId}", ex);
            }
        }

        public async Task<bool> IncreaseQuantityAsync(int userId, int productId)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null) throw new Exception("Cart not found");

                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (cartItem == null) throw new Exception("CartItem not found");

                cartItem.Quantity++;
                cartItem.Price += cartItem.Product.OurPrice;
                cart.TotalPrice = cart.CartItems.Sum(ci => ci.Price);
                await _cartRepository.UpdateAsync(cart);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to increase quantity of product Id: {productId}", ex);
            }
        }

        public async Task<bool> DecreaseQuantityAsync(int userId, int productId)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null) throw new Exception("Cart not found");

                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (cartItem == null) throw new Exception("CartItem not found");

                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    cartItem.Price -= cartItem.Product.OurPrice;
                    cart.TotalPrice = cart.CartItems.Sum(ci => ci.Price);
                    await _cartRepository.UpdateAsync(cart);
                }
                else
                {
                    throw new Exception("Quantity cannot be less than 1");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to Decrease Quantity of product ID: {productId}", ex);
            }
        }
    }
}
