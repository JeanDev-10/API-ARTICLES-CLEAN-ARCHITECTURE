using System;
using Articles.Domain.Exceptions;

namespace Articles.Domain.ValueObjects.Category;

public class CategoryName
{
    public string Value { get; }
    public CategoryName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("El nombre de categoria es requerido.");

        if (value.Length > 100)
            throw new DomainException("El nombre no puede exceder 100 caracteres.");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}
