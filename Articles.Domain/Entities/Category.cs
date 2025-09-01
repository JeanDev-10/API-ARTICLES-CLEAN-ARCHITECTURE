using System;
using Articles.Domain.ValueObjects.Category;

namespace Articles.Domain.Entities;

public class Category : BaseEntity
{
    public CategoryName Name { get; private set; }
    // Navigation Property - Una categoría tiene muchos artículos
    private readonly List<Article> _articles = new();
    public IReadOnlyCollection<Article> Articles => _articles.AsReadOnly();
    public Category(string name)
    {
        Name = new CategoryName(name);
    }
    private Category() { } // Para EF Core

    public void UpdateName(string name)
    {
        Name = new CategoryName(name);
        UpdateTimestamp();
    }
    // Métodos para manejar artículos
    public void AddArticle(Article article)
    {
        if (article == null)
            throw new ArgumentNullException(nameof(article));

        if (!_articles.Contains(article))
        {
            _articles.Add(article);
        }
    }
    public void RemoveArticle(Article article)
    {
        if (article != null)
        {
            _articles.Remove(article);
        }
    }
    public bool HasArticles => _articles.Any();
    public int ArticlesCount => _articles.Count;
}
