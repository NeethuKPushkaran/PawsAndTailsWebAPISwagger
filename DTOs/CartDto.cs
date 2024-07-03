
using System.Diagnostics.Eventing.Reader;

namespace PawsAndTailsWebAPISwagger.DTOs
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }

    public class CreateCartDto
    {
        public int UsertId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class UpdateCartDto
    {
        public int UserId { get; set;}
    }
}
