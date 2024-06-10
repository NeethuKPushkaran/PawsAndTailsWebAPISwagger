
namespace PawsAndTailsWebAPISwagger.DTOs
{
    public class CartDto
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public List<CartItemDto> cartItems { get; set; } = new List<CartItemDto>();
    }
}
