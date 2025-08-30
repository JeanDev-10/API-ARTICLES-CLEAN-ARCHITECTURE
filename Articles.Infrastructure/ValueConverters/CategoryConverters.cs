using System;
using Articles.Domain.ValueObjects.Category;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Articles.Infrastructure.ValueConverters;

public class CategoryConverters
{
    public static readonly ValueConverter<CategoryName, string> NameConverter =
           new(v => v.Value, v => new CategoryName(v));
}
