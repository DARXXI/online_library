using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class BookFilter
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public int? Year { get; set; }

        public string Genre { get; set; }

        public int PageCount { get; set; }
        public int TotalCount { get; set; } = 0;
    }
}
