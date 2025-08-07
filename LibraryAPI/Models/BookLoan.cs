using System;

namespace LibraryAPI.Models;

public class BookLoan
{
    public int Id { get; set; }


    public int BookId { get; set; }
    public virtual Book? Book { get; set; }

    public int MemberId { get; set; }
    public virtual Member? Member { get; set; }
    
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }

}
