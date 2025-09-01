using Articles.Application.DTOs.Category;

namespace Articles.Application.DTOs.Article
{
    public record ArticleDto(int Id, string Name, decimal Price, string Description, DateTime CreatedAt, DateTime UpdatedAt, CategoryDto Category);
    public record CreateArticleDto(string Name, decimal Price, string Description, int CategoryId);
    public record UpdateArticleDto(string Name, decimal Price, string Description, int CategoryId);
}
