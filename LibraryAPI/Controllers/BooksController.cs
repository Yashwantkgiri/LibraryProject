using LibraryAPI.Api.Dtos;
using LibraryAPI.Models;
using LibraryAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ILibraryRepository repository;

        public BooksController(ILibraryRepository repository)
        {
            this.repository = repository;
        }

         // GET: /api/Books
    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await this.repository.GetAllBooksAsync();

    
        var booksDto = books.Select(b => new BookDto
        {
            Id = b.Id,
            Title = b.Title,
            YearOfPublication = b.YearOfPublication,
            AuthorName = b.Author?.Name ?? "N/A"
        });
        // ---------------------

        return Ok(booksDto); 
    }

        [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBookById(int id)
    {
        var book = await this.repository.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
            //return Ok(book);
        // Map the complex Book entity to a simple BookDto for a safe response
        var bookDto = new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            YearOfPublication = book.YearOfPublication,
            AuthorName = book.Author?.Name ?? "N/A"
        };
        return Ok(bookDto);
    }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book cannot be null");
            }

            var addedBook = await this.repository.AddBookAsync(book);
            return CreatedAtAction(nameof(GetBookById), new { id = addedBook.Id }, addedBook);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("Book ID mismatch");
            }

            await this.repository.UpdateBookAsync(id, book);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await this.repository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await this.repository.DeleteBookAsync(id);
            return NoContent();
        }
    }
}
