using System;

namespace LibraryAPI.Models;

public class Member
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }

    public virtual ICollection<BookLoan>? BookLoans { get; set; } = new List<BookLoan>();

}
