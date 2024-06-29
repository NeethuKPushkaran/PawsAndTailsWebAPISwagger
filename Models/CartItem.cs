using System.ComponentModel.DataAnnotations;

namespace PawsAndTailsWebAPISwagger.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required]
        public int CartId {  get; set; }
        public Cart Cart { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public double Price { get; set; }
    }
}
