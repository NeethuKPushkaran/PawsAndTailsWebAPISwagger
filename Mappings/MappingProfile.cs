using AutoMapper;
using PawsAndTailsWebAPISwagger.DTOs;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Map Product to ProductDto and vice versa
            CreateMap<Product, ProductDto>().ReverseMap();

            //Map Cart to CartDto and vice versa
            CreateMap<Cart, CartDto>().ReverseMap();

            //Map CartItem to CartItemDto and vice versa
            CreateMap<CartItem, CartItemDto>().ReverseMap();

            //Map Order to OrderDto and vice versa
            CreateMap<Order, OrderDto>().ReverseMap();

            //Map OrderDetail to OrderDetailDto and vice versa
            CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();

            //Map User to UserDto and vice versa
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
