using Library.Domain.Entity;
using Library.Domain.Enum;

namespace Library.Infrastructure.Repositories.Interface
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<IEnumerable<Book>> SearchAsync(string? searchTerm = null, Genre? genre = null);
        Task AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
        Task<bool> IsIsbnUniqueAsync(string isbn, int? excludeId = null);
    }
}
