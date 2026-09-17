using InventoryApi.Data;
using InventoryApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly InventoryDbContext _context;

        public CategoryService(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }


        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
            
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
             await _context.Categories.AddAsync(category);
             await _context.SaveChangesAsync();
             return category;
        }

        public async Task<Category?> UpdateCategoryAsync(int id, Category category)
        {
            var findCategory = await _context.Categories.FindAsync(id);

            if(findCategory is null)
            {
                return null;
            }

            findCategory.Name = category.Name;
            await _context.SaveChangesAsync();
            return findCategory;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var findCategory = await _context.Categories.FindAsync(id);
            if(findCategory != null) 
            {
                _context.Categories.Remove(findCategory);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        
    }
}
