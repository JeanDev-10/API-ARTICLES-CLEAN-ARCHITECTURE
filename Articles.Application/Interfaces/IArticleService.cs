using System;
using Articles.Application.DTOs.Article;

namespace Articles.Application.Interfaces;

public interface IArticleService
{
    Task<ArticleDto> GetByIdAsync(int id);
    Task<IEnumerable<ArticleDto>> GetAllAsync();
    Task<ArticleDto> CreateAsync(CreateArticleDto dto);
    Task<ArticleDto> UpdateAsync(int id, UpdateArticleDto dto);
    Task DeleteAsync(int id);
}
