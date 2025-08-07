using System;
using LibraryAPI.Models;
using LibraryAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace LibraryAPI.Repositories;

public class LibraryRepository : ILibraryRepository
{
    private readonly LibraryDBContext _context;

    public LibraryRepository(LibraryDBContext context)
    {
        _context = context;
    }


    // Removed duplicate DeleteBookAsync(int id) method to resolve method conflict.

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _context.Books
        .Include(b => b.Author)
        .ToListAsync();
    }

    public Task<IEnumerable<Book>> GetAllBooksAsync(int AuthorId)
    {
        throw new NotImplementedException();
    }

    public Task<Book?> GetBookByIdAsync(int id)
    {
        using (SqlConnection db = new SqlConnection("LibraryDB"))
        {
            db.Open();
            // Perform database operations here
            var sql = @"
        SELECT b.Id, b.Title, a.Name AS AuthorName
        FROM Books b
        INNER JOIN Authors a ON b.AuthorId = a.Id
    ";
            SqlCommand cmd = new SqlCommand(sql, db);
        }
        
 
        // var book =  _context.Books.Include(b => b.Author) .FirstOrDefaultAsync(b => b.Id == id); 

        // if (book == null)
        // {
        //     throw new KeyNotFoundException("Book not found");
        // }
        //return book;

    }

    public async Task<Book> AddBookAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task UpdateBookAsync(int id, Book book)
    {
        if (id != book.Id)
        {
            throw new ArgumentException("Book ID mismatch");
        }

        var existingBook = await _context.Books.FindAsync(id);
        if (existingBook == null)
        {
            throw new KeyNotFoundException("Book not found");
        }

        existingBook.Title = book.Title;
        existingBook.YearOfPublication = book.YearOfPublication;
        existingBook.AuthorId = book.AuthorId;
        {
            _context.Entry(existingBook).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteBookAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
    }

    public Task UpdateBookAsync(int Id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateBookAsync(Book book)
    {
        throw new NotImplementedException();
    }
}
