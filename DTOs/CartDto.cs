
using System.Diagnostics.Eventing.Reader;

namespace PawsAndTailsWebAPISwagger.DTOs
{
    public class CartDto
    {
        //public int CartId { get; set; }
        public int UserId { get; set; }
        public ICollection<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
    }
}
