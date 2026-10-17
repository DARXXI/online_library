using LibrarySystem;
using LibrarySystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public User GetFirst()
        {
            using (var context = new DataBaseContext())
            {
                return context.Users.First();
            }
        }
    }
}
