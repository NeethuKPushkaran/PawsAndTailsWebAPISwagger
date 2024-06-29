using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartItemRepository : IRepository<CartItem>
    {
        Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId);
    }
}
