using System.ComponentModel.DataAnnotations;

namespace PawsAndTailsWebAPISwagger.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required"), StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required"), StringLength(100), EmailAddress(ErrorMessage = "Invalid E-Mail Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required"), StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be atleast 6 Characters long")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsBlocked { get; set; }

        public ICollection<Cart> Carts { get; set; } = new List<Cart>();

        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}
