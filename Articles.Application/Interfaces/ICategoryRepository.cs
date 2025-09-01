using System;
using Articles.Domain.Entities;

namespace Articles.Application.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(int id);
    Task<Category?> GetByNameAsync(string name);
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category> CreateAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task DeleteAsync(int id);
    Task<bool> ExistsByNameAsync(string name, int excludeId = 0);
    Task<int> GetArticleCountAsync(int categoryId);  // Contar artículos
    Task<bool> HasArticlesAsync(int categoryId);  // Verificar si tiene artículos
}
