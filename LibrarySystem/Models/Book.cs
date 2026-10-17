using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }

        public string Author { get; set; }
        public int Year { get; set; }
        public DateTime AddedDate { get; set; }
        public bool IsAvailable { get; set; }

        public override string ToString() => $"{Title} — {Author} ({Year})";
        public int? Year { get; set; }

        public string Genre { get; set; }

        public virtual ICollection<UsersBooks> UserBooks { get; set; }

        public Book()
        {
            UserBooks = new List<UsersBooks>();
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Title)
                && !string.IsNullOrWhiteSpace(Author);
        }
    }
}
