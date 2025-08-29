using Articles.Domain.ValueObjects.Articles;

namespace Articles.Domain.Entities;

public class Article : BaseEntity
{
    public ArticleName Name { get; private set; }
    public ArticlePrice Price { get; private set; }
    public ArticleDescription Description { get; private set; }

    public Article(string name, decimal price, string description)
    {
        Name = new ArticleName(name);
        Price = new ArticlePrice(price);
        Description = new ArticleDescription(description);
    }
    private Article() { } // Para EF Core

    public void UpdateArticle(string name, decimal price, string description)
    {
        Name = new ArticleName(name);
        Price = new ArticlePrice(price);
        Description = new ArticleDescription(description);
    }

}
