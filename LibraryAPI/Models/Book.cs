using System;

namespace LibraryAPI.Models;

public class Book
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int YearOfPublication { get; set; }
    public int AuthorId { get; set; }
    public virtual Author? Author { get; set; }

    public virtual ICollection<BookLoan>? Reviews { get; set; } = new List<BookLoan>();

}
