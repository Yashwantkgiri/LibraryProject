using System;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data;

public class LibraryDBContext : DbContext
{
    public LibraryDBContext(DbContextOptions<LibraryDBContext> options) : base(options)
    {
    }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<BookLoan> BookLoans { get; set; }
    
}
