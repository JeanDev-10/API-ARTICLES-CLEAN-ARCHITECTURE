using Articles.Application.DTOs.Article;
using Articles.Application.Interfaces;
using Articles.Application.Mappers;
using Articles.Domain.Entities;
using FluentValidation;
using static Articles.Domain.Exceptions.DomainException;
namespace Articles.Application.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _repository;
    private readonly ICategoryRepository _categoryRepository;

    public ArticleService(IArticleRepository repository, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
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
        // Verificar que la categoría existe
        var categoryExists = await _categoryRepository.GetByIdAsync(dto.CategoryId);
        if (categoryExists == null)
            throw new InvalidCategoryException(dto.CategoryId);
        // Verificar que el nombre no existe
        if (await _repository.ExistsByNameAsync(dto.Name))
            throw new DuplicateArticleNameException(dto.Name);
        var article = new Article(dto.Name, dto.Price, dto.Description, dto.CategoryId);
        var createdArticle = await _repository.CreateAsync(article);
        // Obtener el artículo con la categoría para el DTO completo
        var articleWithCategory = await _repository.GetByIdAsync(createdArticle.Id);
        return ArticleMapper.ToDto(articleWithCategory);
    }
    public async Task<ArticleDto> UpdateAsync(int id, UpdateArticleDto dto)
    {
        var article = await _repository.GetByIdAsync(id);
        if (article == null)
            throw new ArticleNotFoundException(id);
        // Verificar que la categoría existe
        var categoryExists = await _categoryRepository.GetByIdAsync(dto.CategoryId);
        if (categoryExists == null)
            throw new InvalidCategoryException(dto.CategoryId);

        // Verificar nombre único (excluyendo el actual)
        if (await _repository.ExistsByNameAsync(dto.Name, id))
            throw new DuplicateArticleNameException(dto.Name);

        article.UpdateArticle(dto.Name, dto.Price, dto.Description, dto.CategoryId);
        var updatedArticle = await _repository.UpdateAsync(article);
        // Obtener el artículo con la categoría para el DTO completo
        var articleWithCategory = await _repository.GetByIdAsync(updatedArticle.Id);
        return ArticleMapper.ToDto(articleWithCategory);
    }
    public async Task DeleteAsync(int id)
    {
        var article = await _repository.GetByIdAsync(id);
        if (article == null)
            throw new ArticleNotFoundException(id);
        await _repository.DeleteAsync(id);
    }


}
