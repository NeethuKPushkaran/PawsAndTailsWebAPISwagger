using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<IEnumerable<Cart>> GetCartByUserIdAsync(int userId);
    }
}
