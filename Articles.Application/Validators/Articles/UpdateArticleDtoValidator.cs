using System;
using Articles.Application.DTOs.Article;
using Articles.Application.Interfaces;
using FluentValidation;

namespace Articles.Application.Validators.Articles;

public class UpdateArticleDtoValidator : AbstractValidator<UpdateArticleDto>
{
    private readonly IArticleRepository _repository;

    public UpdateArticleDtoValidator(IArticleRepository repository)
    {
        _repository = repository;

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del artículo es requerido")
            .Length(1, 100).WithMessage("El nombre debe tener entre 1 y 100 caracteres");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El precio debe ser mayor a 0")
            .LessThanOrEqualTo(999999.99m).WithMessage("El precio no puede exceder $999,999.99");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("La descripción es requerida")
            .Length(1, 500).WithMessage("La descripción debe tener entre 1 y 500 caracteres");
    }

    public async Task<bool> BeUniqueNameForUpdate(string name, int articleId, CancellationToken cancellationToken)
    {
        return !await _repository.ExistsByNameAsync(name, articleId);
    }
}
