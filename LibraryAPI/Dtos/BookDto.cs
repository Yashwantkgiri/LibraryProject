namespace LibraryAPI.Api.Dtos;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int YearOfPublication { get; set; }
    public string AuthorName { get; set; } = string.Empty;
}