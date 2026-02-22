using Library.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Library.Infrastructure.Data
{
    public class LibraryBookDbContext : DbContext
    {
        public LibraryBookDbContext(DbContextOptions<LibraryBookDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .ToTable("Book");

            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();


        }
    }
}
