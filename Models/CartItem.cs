using System.ComponentModel.DataAnnotations;

namespace PawsAndTailsWebAPISwagger.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }

        public int CartId {  get; set; }
        public Cart Cart { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
