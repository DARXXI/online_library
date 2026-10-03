using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Data;

namespace LibrarySystem.Repositories
{
    // Один универсальный репозиторий для любой таблицы: Repository<Book>, Repository<Reader>...
    // Под каждую операцию открывается свой короткоживущий LibraryContext — так проще всего
    // избежать путаницы с тем, что EF уже отслеживает одну и ту же запись в двух местах.
    public class Repository<T> where T : class
    {
        public List<T> GetAll()
        {
            using (var context = new LibraryContext())
            {
                return context.Set<T>().ToList();
            }
        }

        public void Add(T entity)
        {
            using (var context = new LibraryContext())
            {
                context.Set<T>().Add(entity);
                context.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (var context = new LibraryContext())
            {
                context.Set<T>().Update(entity);
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (var context = new LibraryContext())
            {
                var entity = context.Set<T>().Find(id);
                if (entity == null) return;

                context.Set<T>().Remove(entity);
                context.SaveChanges();
            }
        }
    }
}
