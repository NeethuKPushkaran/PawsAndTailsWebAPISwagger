using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<Cart> GetCartWithItemsAsync(int cartId);
    }
}
