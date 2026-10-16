using System.Linq;

namespace LibrarySystem.Filters
{
    // Общая часть фильтра для любой таблицы: строка поиска и порядок шагов.
    // Наследник (BookFilter, ReaderFilter) решает только, ПО КАКИМ полям искать,
    // что ещё отбирать и как сортировать (паттерн "Шаблонный метод").
    // Фильтр — это данные из полей формы, поэтому он создаётся через new, а не через DI.
    public abstract class BaseFilter<T> where T : class
    {
        public string Search { get; set; }

        // Всё собирается в IQueryable и выполняется в базе одним SQL-запросом.
        public IQueryable<T> Apply(IQueryable<T> query)
        {
            if (!string.IsNullOrWhiteSpace(Search))
            {
                query = ApplySearch(query, Search.Trim());
            }

            query = ApplyFilters(query);
            return ApplyOrder(query);
        }

        protected abstract IQueryable<T> ApplySearch(IQueryable<T> query, string search);

        // Шаблон "содержит" для SQL LIKE. Обычный string.Contains провайдер Jet переводит
        // в InStr, а он в Access различает регистр: "вой" не нашёл бы "Война и мир".
        // LIKE регистр не различает. Знаки % _ [ из самого текста берутся в [],
        // чтобы искались как обычные символы, а не как часть шаблона.
        protected static string LikePattern(string search)
        {
            string escaped = search.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");
            return "%" + escaped + "%";
        }

        // Дополнительные условия, кроме поиска. По умолчанию их нет.
        protected virtual IQueryable<T> ApplyFilters(IQueryable<T> query)
        {
            return query;
        }

        protected abstract IQueryable<T> ApplyOrder(IQueryable<T> query);
    }
}
