
using System.Diagnostics.Eventing.Reader;

namespace PawsAndTailsWebAPISwagger.DTOs
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public double TotalPrice { get; set; }
        public List<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
    }
}
