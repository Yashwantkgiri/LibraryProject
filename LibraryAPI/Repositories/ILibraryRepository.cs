using System;
using LibraryAPI.Models;

namespace LibraryAPI.Repositories;

public interface ILibraryRepository
{
    Task<IEnumerable<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);

    Task<Book> AddBookAsync(Book book);
    Task UpdateBookAsync(int Id, Book book);
    Task DeleteBookAsync(int id);
    Task UpdateBookAsync(Book book);
}
