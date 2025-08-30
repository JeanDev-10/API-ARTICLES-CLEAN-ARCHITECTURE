using System;
using Articles.Application.DTOs.Category;
using Articles.Application.Interfaces;
using FluentValidation;

namespace Articles.Application.Validators.Categories;

public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
{
    private readonly ICategoryRepository _repository;

    public UpdateCategoryDtoValidator(ICategoryRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la categoría es requerido")
            .Length(1, 100).WithMessage("El nombre debe tener entre 1 y 100 caracteres");
    }

    public async Task<bool> BeUniqueNameForUpdate(string name, int categoryId, CancellationToken cancellationToken)
    {
        return !await _repository.ExistsByNameAsync(name, categoryId);
    }
}
