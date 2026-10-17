using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Models
{
    public class User : BaseEntity
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<UsersBooks> UsersBooks { get; set; }

        public User()
        {
            UsersBooks = new List<UsersBooks>();
        }
    }
}
