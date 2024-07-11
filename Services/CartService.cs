using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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
        private readonly ICartItemRepository _cartItemRepository;

        public CartService(ICartRepository cartRepository, IMapper mapper, IProductRepository productRepository, ICartItemRepository cartItemRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _cartItemRepository = cartItemRepository;
        }

        //public async Task<IEnumerable<CartDto>> GetAllCartsAsync()
        //{
        //    try
        //    {
        //        var carts = await _cartRepository.GetAllAsync();
        //        return _mapper.Map<IEnumerable<CartDto>>(carts);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Failed to retrieve All Carts", ex);
        //    }
        //}

        //public async Task<CartDto> GetCartByIdAsync(int CartId)
        //{
        //    try
        //    {
        //        var cart = await _cartRepository.GetByIdAsync(CartId);
        //        if (cart == null) throw new Exception("Cart not found");
        //        return _mapper.Map<CartDto>(cart);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"Failed to retrieve cart by id", ex);
        //    }
        //}
        public async Task<CartDto> GetCartByUserIdAsync(int userId)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null) throw new Exception("Cart not found");
                return _mapper.Map<CartDto>(cart);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to retrieve cart with User ID: {userId}", ex);
            }
        }

        public async Task AddToCartAsync(int userId, int productId, int quantity)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(productId);
                if (product == null)
                {
                    throw new ArgumentException("Product not found");
                }

                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = userId,
                        CreatedAt = DateTime.Now,
                        CartItems = new List<CartItem>()
                    };
                    await _cartRepository.AddAsync(cart);
                }

                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                    cartItem.Price += product.OurPrice * quantity;
                }
                else
                {
                    cart.CartItems.Add(new CartItem
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        Price = product.OurPrice * quantity
                    });
                }

                await _cartRepository.UpdateAsync(cart);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while adding to the cart: {ex.Message}", ex);
            }
        }

        //public async Task UpdateCartAsync(int cartId, CartDto cartDto)
        //{
        //    try
        //    {
        //        var cart = await _cartRepository.GetByIdAsync(cartId);
        //        if (cart == null) throw new CartNotFoundException("Cart not found");

        //        cart.UserId = cartDto.UserId;
        //        cart.CreatedAt = cartDto.CreatedAt;

        //        foreach (var cartItemDto in cartDto.CartItems)
        //        {
        //            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == cartItemDto.CartItemId);
        //            if (cartItem == null)
        //            {
        //                cartItem = _mapper.Map<CartItem>(cartItemDto);
        //                cart.CartItems.Add(cartItem);
        //            }
        //            else
        //            {
        //                _mapper.Map(cartItemDto, cartItem);
        //            }
        //        }

        //        cart.TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Price);

        //        await _cartRepository.UpdateAsync(cart);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Failed to update cart", ex);
        //    }
        //}

        //public async Task RemoveFromCartAsync(int cartId, int productId)
        //{
        //    try
        //    {
        //        var cart = await _cartRepository.GetByIdAsync(cartId);
        //        if (cart == null) throw new Exception("Cart not found");

        //        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
        //        if (cartItem != null)
        //        {
        //            cart.CartItems.Remove(cartItem);
        //            cart.TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Price);
        //            await _cartRepository.UpdateAsync(cart);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception($"Failed to delete cart with ID {cartId}", ex);
        //    }
        //}


        public async Task IncreaseQuantityAsync(int cartItemId)
        {
            try
            {
                var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
                if (cartItem != null)
                {
                    cartItem.Quantity++;
                    cartItem.Price += cartItem.Product.OurPrice;
                    await _cartItemRepository.UpdateAsync(cartItem);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to increase quantity of cartitem", ex);
            }
        }

        public async Task DecreaseQuantityAsync(int cartItemId)
        {
            try
            {
                var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
                if (cartItem != null && cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    cartItem.Price -= cartItem.Product.OurPrice;
                    await _cartItemRepository.UpdateAsync(cartItem);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to Decrease Quantity", ex);
            }
        }

        //public async Task DoCheckoutAsync(int cartId)
        //{
        //    try
        //    {
        //        var cart = await _cartRepository.GetByIdAsync(cartId);
        //        if (cart != null)
        //        {
        //            await _orderService.CreateOrderAsync(cart);

        //            // Clear the cart
        //            foreach (var cartItem in cart.CartItems.ToList())
        //            {
        //                await _cartItemRepository.DeleteAsync(cartItem);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception($"failed to docheckout for cart id: {cartId}", ex);
        //    }
        //}
    }
}
