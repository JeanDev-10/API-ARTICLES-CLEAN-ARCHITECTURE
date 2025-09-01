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
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("El nombre de la categoría es requerido")
            .Length(1, 100).WithMessage("El nombre debe tener entre 1 y 100 caracteres")
            .MustAsync(BeUniqueName).WithMessage("Ya existe una categoría con este nombre");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            return true; // no validar uniqueness si está vacío, FluentValidation ya atrapará NotEmpty
        return !await _repository.ExistsByNameAsync(name);
    }
}
