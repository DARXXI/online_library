using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Helpers
{
    public class BookFilter
    {
            public Book GetBookById(int bk)
            {
                using (var context = new DataBaseContext())
                {
                    return new Book { };
                // return context.Books.SingleOrDefault(x => x.Id == id);
                }
            }
    }
}
