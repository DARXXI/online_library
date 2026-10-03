using System;

namespace LibrarySystem.Models
{
    // Таблица-связка Books <-> Readers: одна запись = одна выдача книги читателю.
    public class Loan : BaseEntity
    {
        public int BookId { get; set; }
        public int ReaderId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
