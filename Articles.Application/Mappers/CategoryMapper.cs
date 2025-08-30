using System;
using Articles.Application.DTOs.Category;
using Articles.Domain.Entities;

namespace Articles.Application.Mappers;

public class CategoryMapper
{
    public static CategoryDto ToDto(Category category)
    {
        return new CategoryDto(
                 category.Id,
                 category.Name.ToString(),
                 category.CreatedAt,
                 category.UpdatedAt
             );
    }
}
