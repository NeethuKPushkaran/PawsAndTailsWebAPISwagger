using Microsoft.EntityFrameworkCore;
using PawsAndTailsWebAPISwagger.Data;
using PawsAndTailsWebAPISwagger.Interfaces;
using PawsAndTailsWebAPISwagger.Models;

namespace PawsAndTailsWebAPISwagger.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

       public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            try
            {
                return await _context.Set<Cart>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve carts", ex);
            }
        }

        public async Task<Cart> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Cart>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to retrieve cart with ID {id}", ex);
            }
        }

        public async Task AddAsync(Cart entity)
        {
            try
            {
                await _context.Set<Cart>().AddAsync(entity);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to add to cart", ex);
            }
        }

        public async Task UpdateAsync(Cart entity)
        {
            try
            {
                _context.Set<Cart>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("FAiled to update cart", ex);
            }
        }

        public async Task DeleteAsync(Cart entity)
        {            
            try
            {
                if(entity != null)
                {
                    _context.Carts.Remove(entity);
                    await _context.SaveChangesAsync();
                }
                
            }
            catch(Exception ex)
            {
                throw new Exception("Failed to delete cart", ex);
            }
        }

        public async Task<Cart> GetCartWithItemsAsync(int cartId)
        {
            try
            {
                return await _context.Set<Cart>()
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                    .FirstOrDefaultAsync(c => c.CartId == cartId);
            }
            catch(Exception ex)
            {
                throw new Exception($"Failed to retrieve carts", ex);
            }
        }
    }
}
