using System;
using Articles.Application.DTOs.Article;
using Articles.Application.Interfaces;
using FluentValidation;

namespace Articles.Application.Validators.Articles;

public class CreateArticleDtoValidator : AbstractValidator<CreateArticleDto>
{
    private readonly IArticleRepository _repository;
    private readonly ICategoryRepository _categoryRepository;

    public CreateArticleDtoValidator(IArticleRepository repository, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("El nombre del artículo es requerido")
            .Length(1, 100).WithMessage("El nombre debe tener entre 1 y 100 caracteres")
            .MustAsync(BeUniqueName).WithMessage("Ya existe un artículo con este nombre");

        RuleFor(x => x.Price)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0")
            .LessThanOrEqualTo(999999.99m).WithMessage("El precio no puede exceder $999,999.99");

        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("La descripción es requerida")
            .Length(1, 500).WithMessage("La descripción debe tener entre 1 y 500 caracteres");
        RuleFor(x => x.CategoryId)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("Debe seleccionar una categoría")
            .GreaterThan(0).WithMessage("Debe seleccionar una categoría válida")
            .MustAsync(CategoryExists).WithMessage("La categoría seleccionada no existe");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > 100)
            return true; // no validar uniqueness si está vacío, FluentValidation ya atrapará NotEmpty
        return !await _repository.ExistsByNameAsync(name);
    }
    private async Task<bool> CategoryExists(int categoryId, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        return category != null;
    }
}
