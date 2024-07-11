namespace PawsAndTailsWebAPISwagger.Models
{
    public class Wishlist
    {
        public int WishlistId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }
    }
}
