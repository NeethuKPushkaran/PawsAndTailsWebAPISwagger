namespace PawsAndTailsWebAPISwagger.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public double OrigPrice { get; set; }
        public double OurPrice { get; set; }
        public double Rating { get; set; }
        public int Stock {  get; set; }
    }
}
