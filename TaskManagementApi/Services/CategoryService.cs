using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;


namespace TaskManagement.Services;
public class CategoryService
{
    private readonly AppDbContext _context;
    public CategoryService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetCategories()
    {
        return await _context.Categories.ToListAsync();
    }
    
    public async Task<Category?> GetCategoryById(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(e=>e.Id==id);
    }

    public async Task<Category> AddCategory(CreateCategoryRequest request)
    {
        Category? newCategory = new();
        newCategory.Name = request.Name;
        await _context.Categories.AddAsync(newCategory);
        await _context.SaveChangesAsync();
        return newCategory;
    }

    public async Task<bool> UpdateCategory(int id, UpdateCategoryRequest request)
    {
        Category? changeCategory = await _context.Categories.FirstOrDefaultAsync(e=>e.Id==id);
        if (changeCategory == null)
        {
            return false;
        }
        changeCategory.Name = request.Name;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCategory(int id)
    {
        Category? deleteCategory =  await _context.Categories.FirstOrDefaultAsync(e=>e.Id == id);
        if (deleteCategory == null)
        {
            return false;
        }
        _context.Categories.Remove(deleteCategory);
        await _context.SaveChangesAsync();
        return true;
    }
}