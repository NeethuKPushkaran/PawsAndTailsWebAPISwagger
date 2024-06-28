using System.ComponentModel.DataAnnotations;

namespace PawsAndTailsWebAPISwagger.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username is required"), StringLength(50)]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Email is required"), StringLength(100), EmailAddress(ErrorMessage = "Invalid E-Mail Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required"), StringLength(100, MinimumLength = 6, ErrorMessage = "Password must by at least 6 characters long")]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set;}
        public string Role { get; set; }

    }
}
