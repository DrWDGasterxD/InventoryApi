using InventoryApi.Data;
using InventoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Services
{
    public class ProductService : IProductService
    {
        private readonly InventoryDbContext _context;

        public ProductService(InventoryDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(f => f.Id == id);
             
        }

        public async Task<Product?> CreateProductAsync(Product product)
        {
            var category = await _context.Categories.FindAsync(product.CategoryId);

            if(category is null)
            {
                return null;
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            var findProduct = await _context.Products.FindAsync(id);

            if(findProduct is null)
            {
                return null;
            }

            findProduct.Name = product.Name;
            findProduct.Price = product.Price;
            findProduct.Stock = product.Stock;
            findProduct.CategoryId = product.CategoryId;

            await _context.SaveChangesAsync();
            return findProduct;
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var findProduct = await _context.Products.FindAsync(id);
            if (findProduct != null)
            {
                _context.Products.Remove(findProduct);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
