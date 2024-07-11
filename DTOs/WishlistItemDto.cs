namespace PawsAndTailsWebAPISwagger.DTOs
{
    public class WishlistItemDto
    {
        public int WishlistItemId { get; set; }
        public int WishlistId { get; set; }
        public int ProductId { get; set; }
        public ProductDto Product { get; set; }
    }
}
