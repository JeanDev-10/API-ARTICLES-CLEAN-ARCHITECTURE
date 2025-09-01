using Articles.Application.DTOs.Category;
using Articles.Application.Interfaces;
using Articles.Application.Mappers;
using Articles.Application.Validators.Categories;
using Articles.Domain.Entities;
using FluentValidation;
using static Articles.Domain.Exceptions.DomainException;
namespace Articles.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    public CategoryService(
        ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            throw new CategoryNotFoundException(id);

        return CategoryMapper.ToDto(category);
    }
    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(CategoryMapper.ToDto);
    }
    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category(dto.Name);
        var createdCategory = await _repository.CreateAsync(category);

        return CategoryMapper.ToDto(createdCategory);
    }
    public async Task<CategoryDto> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            throw new CategoryNotFoundException(id);
        category.UpdateName(dto.Name);
        var updatedCategory = await _repository.UpdateAsync(category);
        return CategoryMapper.ToDto(updatedCategory);
    }
    public async Task DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            throw new CategoryNotFoundException(id);
        // Verificar si tiene artículos
        if (await _repository.HasArticlesAsync(id))
            throw new CategoryHasArticlesException();
        await _repository.DeleteAsync(id);
    }

}
