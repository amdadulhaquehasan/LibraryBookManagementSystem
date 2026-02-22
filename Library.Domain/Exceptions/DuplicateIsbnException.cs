namespace Library.Domain.Exceptions
{
    public class DuplicateIsbnException : Exception
    {
        public DuplicateIsbnException(string isbn)
            : base($"A book with ISBN '{isbn}' already exists.")
        {
        }
    }
}
