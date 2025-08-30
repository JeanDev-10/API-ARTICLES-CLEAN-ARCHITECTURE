using System;
using Articles.Application.Interfaces;
using Articles.Domain.Entities;
using Articles.Domain.ValueObjects.Category;
using Articles.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Articles.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<Category?> GetByNameAsync(string name)
    {
        var nameVo = new CategoryName(name);
        return await _context.Categories.FirstOrDefaultAsync(c => c.Name == nameVo);
    }
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }
    public async Task<Category> CreateAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }
    public async Task<Category> UpdateAsync(Category category)
    {
        _context.Entry(category).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return category;
    }
    public async Task DeleteAsync(int id)
    {
        var category = await GetByIdAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByNameAsync(string name, int excludeId = 0)
    {
        var nameVo = new CategoryName(name);
        return await _context.Categories.AnyAsync(c => c.Name == nameVo && c.Id != excludeId);
    }
}
