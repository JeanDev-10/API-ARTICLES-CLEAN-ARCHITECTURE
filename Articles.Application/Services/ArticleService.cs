using Articles.Application.DTOs.Article;
using Articles.Application.Interfaces;
using Articles.Application.Mappers;
using Articles.Application.Validators.Articles;
using Articles.Domain.Entities;
using FluentValidation;
using static Articles.Domain.Exceptions.DomainException;
namespace Articles.Application.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _repository;
    private readonly IValidator<CreateArticleDto> _createValidator;
    private readonly IValidator<UpdateArticleDto> _updateValidator;

    public ArticleService(IArticleRepository repository, IValidator<CreateArticleDto> createValidator, IValidator<UpdateArticleDto> updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }
    public async Task<ArticleDto> GetByIdAsync(int id)
    {
        var article = await _repository.GetByIdAsync(id);
        if (article == null)
            throw new ArticleNotFoundException(id);

        return ArticleMapper.ToDto(article);
    }
    public async Task<IEnumerable<ArticleDto>> GetAllAsync()
    {
        var articles = await _repository.GetAllAsync();
        return articles.Select(ArticleMapper.ToDto);
    }
    public async Task<ArticleDto> CreateAsync(CreateArticleDto dto)
    {
        // Validar con FluentValidation
        var validationResult = await _createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errors);
        }
        // Verificar que el nombre no existe
        if (await _repository.ExistsByNameAsync(dto.Name))
            throw new DuplicateArticleNameException(dto.Name);

        var article = new Article(dto.Name, dto.Price, dto.Description);
        var createdArticle = await _repository.CreateAsync(article);

        return ArticleMapper.ToDto(createdArticle);
    }
    public async Task<ArticleDto> UpdateAsync(int id, UpdateArticleDto dto)
    {
        var article = await _repository.GetByIdAsync(id);
        if (article == null)
            throw new ArticleNotFoundException(id);

        // Validar con FluentValidation
        var validationResult = await _updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errors);
        }
        // Validar nombre único para actualización
        var validator = _updateValidator as UpdateArticleDtoValidator;
        if (validator != null)
        {
            var isUniqueForUpdate = await validator.BeUniqueNameForUpdate(dto.Name, id, CancellationToken.None);
            if (!isUniqueForUpdate)
            {
                throw new DuplicateArticleNameException(dto.Name);
            }
        }
        article.UpdateArticle(dto.Name, dto.Price, dto.Description);
        var updatedArticle = await _repository.UpdateAsync(article);
        return ArticleMapper.ToDto(updatedArticle);
    }
    public async Task DeleteAsync(int id)
    {
        var article = await _repository.GetByIdAsync(id);
        if (article == null)
            throw new ArticleNotFoundException(id);

        await _repository.DeleteAsync(id);
    }


}
