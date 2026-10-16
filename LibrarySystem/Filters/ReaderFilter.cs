using System.Linq;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Models;

namespace LibrarySystem.Filters
{
    public class ReaderFilter : BaseFilter<Reader>
    {
        protected override IQueryable<Reader> ApplySearch(IQueryable<Reader> query, string search)
        {
            string pattern = LikePattern(search);
            return query.Where(r =>
                EF.Functions.Like(r.FullName, pattern) ||
                EF.Functions.Like(r.Phone, pattern) ||
                EF.Functions.Like(r.Email, pattern));
        }

        protected override IQueryable<Reader> ApplyOrder(IQueryable<Reader> query)
        {
            return query.OrderBy(r => r.FullName);
        }
    }
}
