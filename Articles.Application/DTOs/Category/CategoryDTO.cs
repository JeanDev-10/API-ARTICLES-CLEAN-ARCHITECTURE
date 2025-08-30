namespace Articles.Application.DTOs.Category
{
    public record CategoryDto(int Id, string Name, DateTime CreatedAt, DateTime UpdatedAt);
    public record CreateCategoryDto(string Name);
    public record UpdateCategoryDto(string Name);
}