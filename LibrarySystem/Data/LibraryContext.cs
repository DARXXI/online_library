using Microsoft.EntityFrameworkCore;
using LibrarySystem.Models;

namespace LibrarySystem.Data
{
    // DbContext — это и есть "репозиторий": DbSet<T> сам умеет загружать, добавлять
    // и удалять записи, а запросы пишутся через LINQ (Where/OrderBy/...), а не через
    // ручной SQL. EntityFrameworkCore.Jet.OleDb — провайдер EF Core для MS Access.
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Reader> Readers => Set<Reader>();
        public DbSet<Loan> Loans => Set<Loan>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseJetOleDb(DbHelper.ConnectionString);
        }
    }
}
