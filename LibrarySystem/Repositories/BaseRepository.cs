using Microsoft.EntityFrameworkCore;
using LibrarySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Repositories
{
    public class BaseRepository<T> where T : class
    {
        public List<T> GetAll()
        {
            using (var context = new DataBaseContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public void Add(T entity)
        {
            using (var context = new DataBaseContext())
            {
                context.Update(entity);
                context.SaveChangesAsync(); 
            }
        }

        public void Update(T entity)
        {
            using (var context = new DataBaseContext())
            {
                context.Set<T>().Update(entity);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new DataBaseContext())
            {
                var entity = context.Set<T>().Find(id);
                if (entity == null) return;

                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
