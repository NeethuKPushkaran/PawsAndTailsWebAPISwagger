using System.ComponentModel.DataAnnotations;

namespace PawsAndTailsWebAPISwagger.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, StringLength(50)]
        public string UserName { get; set; }

        [Required,StringLength(100), EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(100)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<Cart> Carts { get; set; } = new List<Cart>();

        public ICollection<Order> Orders { get; set; }

    }
}
