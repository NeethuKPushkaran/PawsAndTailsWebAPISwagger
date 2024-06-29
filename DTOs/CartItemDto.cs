namespace PawsAndTailsWebAPISwagger.DTOs
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageUrl { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
}
