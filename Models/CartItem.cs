using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawsAndTailsWebAPISwagger.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int CartId {  get; set; }

        [ForeignKey("CartId")]
        public Cart Cart { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        public double Price { get; set; }
    }
}
