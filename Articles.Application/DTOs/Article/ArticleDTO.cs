namespace Articles.Application.DTOs.Article
{
    public record ArticleDto(int Id, string Name, decimal Price, string Description, DateTime CreatedAt, DateTime UpdatedAt);
    public record CreateArticleDto(string Name, decimal Price, string Description);
    public record UpdateArticleDto(string Name, decimal Price, string Description);
}
