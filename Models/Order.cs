using System.ComponentModel.DataAnnotations;

namespace PawsAndTailsWebAPISwagger.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
