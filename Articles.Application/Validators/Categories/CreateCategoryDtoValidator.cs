using System;
using Articles.Application.DTOs.Category;
using Articles.Application.Interfaces;
using FluentValidation;

namespace Articles.Application.Validators.Categories;

public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
{
    private readonly ICategoryRepository _repository;

    public CreateCategoryDtoValidator(ICategoryRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre de la categoría es requerido")
            .Length(1, 100).WithMessage("El nombre debe tener entre 1 y 100 caracteres")
            .MustAsync(BeUniqueName).WithMessage("Ya existe una categoría con este nombre");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return !await _repository.ExistsByNameAsync(name);
    }
}
