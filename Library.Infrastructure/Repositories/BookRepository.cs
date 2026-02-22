using Library.Domain.Entity;
using Library.Domain.Enum;
using Library.Domain.Exceptions;
using Library.Infrastructure.Data;
using Library.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryBookDbContext _context;

        public BookRepository(LibraryBookDbContext context)
        {
            _context = context;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchAsync(string? searchTerm = null, Genre? genre = null)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(b => 
                b.Title.Contains(searchTerm) || 
                b.ISBN.Contains(searchTerm));
            }

            if (genre.HasValue)
            {
                query = query.Where(b => b.Genre == genre);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(Book book)
        {
            if (!await IsIsbnUniqueAsync(book.ISBN))
                throw new DuplicateIsbnException(book.ISBN);

            if (book.PublicationYear > DateTime.UtcNow.Year + 5 )
                throw new InvalidPublicationYearException(book.PublicationYear);

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            var existing = await _context.Books.FindAsync(book.Id);

            if(existing == null)
                throw new BookNotFoundException(book.Id);

            if(!await IsIsbnUniqueAsync(book.ISBN, book.Id))
                throw new DuplicateIsbnException(book.ISBN);

            existing.Title = book.Title;
            existing.ISBN = book.ISBN;
            existing.PublicationYear = book.PublicationYear;
            existing.Pages = book.Pages;
            existing.Genre = book.Genre;
            existing.IsAvailable = book.IsAvailable;
            existing.LastUpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                throw new BookNotFoundException(id);

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsIsbnUniqueAsync(string isbn, int? excludeId = null)
        {
            return !await _context.Books
                .AnyAsync(b => b.ISBN == isbn && (!excludeId.HasValue || b.Id != excludeId));
        }
    }
}
