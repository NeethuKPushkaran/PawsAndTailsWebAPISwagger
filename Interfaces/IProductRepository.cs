using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task RemoveProductAsync(int id);
    }
}
