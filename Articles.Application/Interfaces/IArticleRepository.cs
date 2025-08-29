using Articles.Domain.Entities;

namespace Articles.Application.Interfaces;

public interface IArticleRepository
{
    Task<Article?> GetByIdAsync(int id);
    Task<Article?> GetByNameAsync(string name);
    Task<IEnumerable<Article>> GetAllAsync();
    Task<Article> CreateAsync(Article article);
    Task<Article> UpdateAsync(Article article);
    Task DeleteAsync(int id);
    Task<bool> ExistsByNameAsync(string name, int excludeId = 0);
}
