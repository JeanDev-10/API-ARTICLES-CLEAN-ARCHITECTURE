using System;
using Articles.Domain.ValueObjects.Category;

namespace Articles.Domain.Entities;

public class Category : BaseEntity
{
    public CategoryName Name { get; private set; }
    public Category(string name)
    {
        Name = new CategoryName(name);
    }
    private Category() { } // Para EF Core

    public void UpdateName(string name)
    {
        Name = new CategoryName(name);
    }
}
