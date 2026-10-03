using System;

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
    }
}
