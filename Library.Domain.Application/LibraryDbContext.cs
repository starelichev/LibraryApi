using Library.Domain.Application.Models;

namespace Library.Domain.Application;

using Microsoft.EntityFrameworkCore;

public class LibraryDbContext : DbContext
{ 
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Reader> Readers { get; set; }
    public DbSet<History> Histories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasOne(a => a.Author).WithMany(a=>a.Books).HasForeignKey(a=>a.AuthorId);
        base.OnModelCreating(modelBuilder);
    }
}