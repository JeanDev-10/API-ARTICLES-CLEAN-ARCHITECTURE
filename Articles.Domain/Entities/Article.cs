using Articles.Domain.ValueObjects.Articles;

namespace Articles.Domain.Entities;

public class Article : BaseEntity
{
    public ArticleName Name { get; private set; }
    public ArticlePrice Price { get; private set; }
    public ArticleDescription Description { get; private set; }
    // Foreign Key y Navigation Property
    public int CategoryId { get; private set; }
    public Category Category { get; private set; }


    public Article(string name, decimal price, string description, int categoryId)
    {
        validateCategoryId(categoryId);
        Name = new ArticleName(name);
        Price = new ArticlePrice(price);
        Description = new ArticleDescription(description);
        CategoryId = categoryId;
    }
    private Article() { } // Para EF Core

    public void UpdateArticle(string name, decimal price, string description, int categoryId)
    {
        validateCategoryId(categoryId);
        Name = new ArticleName(name);
        Price = new ArticlePrice(price);
        Description = new ArticleDescription(description);
        CategoryId = categoryId;
        UpdateTimestamp();
    }

    private static void validateCategoryId(int categoryId)
    {
        if (categoryId <= 0)
            throw new ArgumentException("CategoryId debe ser un entero positivo.", nameof(categoryId));
    }

}
