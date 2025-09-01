using Articles.Application.DTOs.Article;

namespace Articles.Application.DTOs.Category
{
    public record CategoryDto(int Id, string Name, int ArticlesCount, DateTime CreatedAt, DateTime UpdatedAt);
    public record CreateCategoryDto(string Name);
    public record UpdateCategoryDto(string Name);
    // DTO simple para listados
    public record CategorySummaryDto(int Id, string Name, DateTime CreatedAt, DateTime UpdatedAt);
}