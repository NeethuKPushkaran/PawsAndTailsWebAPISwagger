using System.ComponentModel.DataAnnotations;

namespace PawsAndTailsWebAPISwagger.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }

        public double TotalPrice { get; set; }

        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
