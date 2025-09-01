using System;
using Articles.Application.Interfaces;
using Articles.Domain.Entities;
using Articles.Domain.ValueObjects.Articles;
using Articles.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Articles.Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly AppDbContext _context;

    public ArticleRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<Article?> GetByIdAsync(int id)
    {
         return await _context.Articles
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.Id == id);
    }
    public async Task<Article?> GetByNameAsync(string name)
    {
        return await _context.Articles.FirstOrDefaultAsync(a => a.Name.Value == name);
    }

    public async Task<IEnumerable<Article>> GetAllAsync()
    {
        return await _context.Articles.Include(a=>a.Category).ToListAsync();
    }
    public async Task<Article> CreateAsync(Article article)
    {
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
        return article;
    }

    public async Task<Article> UpdateAsync(Article article)
    {
        _context.Entry(article).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return article;
    }

    public async Task DeleteAsync(int id)
    {
        var article = await GetByIdAsync(id);
        if (article != null)
        {
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsByNameAsync(string name, int excludeId = 0)
    {
        var nameVo = new ArticleName(name);
        return await _context.Articles.AnyAsync(a => a.Name == nameVo && a.Id != excludeId);
    }
    public async Task<bool> CategoryExistsAsync(int categoryId)
    {
        return await _context.Categories.AnyAsync(c => c.Id == categoryId);
    }
}
