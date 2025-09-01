using System;
using Articles.Application.DTOs.Article;
using Articles.Application.DTOs.Category;
using Articles.Domain.Entities;

namespace Articles.Application.Mappers;

public static class ArticleMapper
{
    public static ArticleDto ToDto(Article article)
    {
        return new ArticleDto(
            article.Id,
            article.Name.ToString(),
            article.Price.Value,
            article.Description.ToString(),
            article.CreatedAt,
            article.UpdatedAt,
            article.Category != null ?  CategoryMapper.ToDto(article.Category) : null
        );
    }
}
