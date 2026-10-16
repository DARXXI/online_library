using System.Linq;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Models;

namespace LibrarySystem.Filters
{
    public class BookFilter : BaseFilter<Book>
    {
        // null — все книги, true — только в наличии, false — только выданные.
        public bool? IsAvailable { get; set; }

        public int? YearFrom { get; set; }
        public int? YearTo { get; set; }

        protected override IQueryable<Book> ApplySearch(IQueryable<Book> query, string search)
        {
            string pattern = LikePattern(search);
            return query.Where(b => EF.Functions.Like(b.Title, pattern) || EF.Functions.Like(b.Author, pattern));
        }

        protected override IQueryable<Book> ApplyFilters(IQueryable<Book> query)
        {
            if (IsAvailable.HasValue)
            {
                bool available = IsAvailable.Value;
                query = query.Where(b => b.IsAvailable == available);
            }

            if (YearFrom.HasValue)
            {
                int from = YearFrom.Value;
                query = query.Where(b => b.Year >= from);
            }

            if (YearTo.HasValue)
            {
                int to = YearTo.Value;
                query = query.Where(b => b.Year <= to);
            }

            return query;
        }

        protected override IQueryable<Book> ApplyOrder(IQueryable<Book> query)
        {
            return query.OrderBy(b => b.Title);
        }
    }
}
