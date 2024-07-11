namespace PawsAndTailsWebAPISwagger.DTOs
{
    public class WishlistDto
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<WishlistItemDto> WishlistItems { get; set; }
    }
}
