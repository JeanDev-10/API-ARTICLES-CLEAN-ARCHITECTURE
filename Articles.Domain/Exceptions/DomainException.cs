namespace Articles.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
    public DomainException(string message, Exception innerException) : base(message, innerException) { }
    public class ArticleNotFoundException : DomainException
    {
        public ArticleNotFoundException(int id) : base($"Artículo con ID {id} no encontrado") { }
    }

    public class DuplicateArticleNameException : DomainException
    {
        public DuplicateArticleNameException(string name) : base($"Ya existe un artículo con el nombre '{name}'") { }
    }
    public class CategoryNotFoundException : DomainException
    {
        public CategoryNotFoundException(int id) : base($"Categoría con ID {id} no encontrado") { }
    }

    public class DuplicateCategoryNameException : DomainException
    {
        public DuplicateCategoryNameException(string name) : base($"Ya existe una categoría con el nombre '{name}'") { }
    }
}
