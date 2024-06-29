
using System.Diagnostics.Eventing.Reader;

namespace PawsAndTailsWebAPISwagger.DTOs
{
    public class CartDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
        public ICollection<CartItemDto> CartItems { get; set; } = new List<CartItemDto>();
    }
}
