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
    private readonly IValidator<CreateCategoryDto> _createValidator;
    private readonly IValidator<UpdateCategoryDto> _updateValidator;

    public CategoryService(
        ICategoryRepository repository,
        IValidator<CreateCategoryDto> createValidator,
        IValidator<UpdateCategoryDto> updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
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
        // Validar con FluentValidation
        var validationResult = await _createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errors);
        }

        var category = new Category(dto.Name);
        var createdCategory = await _repository.CreateAsync(category);

        return CategoryMapper.ToDto(createdCategory);
    }
    public async Task<CategoryDto> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            throw new CategoryNotFoundException(id);

        // Validar con FluentValidation
        var validationResult = await _updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errors);
        }
        // Validar nombre único para actualización
        var validator = _updateValidator as UpdateCategoryDtoValidator;
        if (validator != null)
        {
            var isUniqueForUpdate = await validator.BeUniqueNameForUpdate(dto.Name, id, CancellationToken.None);
            if (!isUniqueForUpdate)
            {
                throw new DuplicateArticleNameException(dto.Name);
            }
        }
        category.UpdateName(dto.Name);
        var updatedCategory = await _repository.UpdateAsync(category);

        return CategoryMapper.ToDto(updatedCategory);
    }
    public async Task DeleteAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            throw new CategoryNotFoundException(id);

        await _repository.DeleteAsync(id);
    }

}
