namespace PawsAndTailsWebAPISwagger.DTOs
{
    public class CartItemDto
    {
        public int CartItemId { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public ProductDto Product { get; set; }
    }

    public class CreateCartItemDto
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    public class UpdateCartItemDto
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}

