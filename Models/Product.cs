using System.ComponentModel.DataAnnotations;

namespace PawsAndTailsWebAPISwagger.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string ImgUrl { get; set; }

        [Required]
        public double OrigPrice { get; set; }

        [Required]
        public double OurPrice { get; set; }
        public double Rating { get; set; }

        [Required]
        [Range (0, 10000)]
        public int Stock {  get; set; }
        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
