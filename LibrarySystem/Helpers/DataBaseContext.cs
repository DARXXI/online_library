using Microsoft.EntityFrameworkCore;
using LibrarySystem.Models;
using System;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace LibrarySystem
{
    public class DataBaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<UsersBooks> UsersBooks { get; set; }

        private const string DbFileName = "OnlineLibrary.accdb";
        public static string DbPath => Path.Combine(Application.StartupPath, DbFileName);
        public static string ConnectionString =>
            $"Provider=Microsoft.ACE.OLEDB.16.0;Data Source={DbPath};";

        public DataBaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseJetOleDb(ConnectionString);                
        }

        public static OleDbConnection GetConnection()
        {   
            return new OleDbConnection(ConnectionString);
        }

        public static bool TryEnsureDatabaseReady(out string errorMessage)
        {
            errorMessage = null;

            if (!File.Exists(DbPath))
            {
                errorMessage =
                    "Файл базы данных не найден:" + Environment.NewLine + DbPath + Environment.NewLine + Environment.NewLine +
                    "Убедитесь, что файл Library.accdb находится рядом с .exe файлом программы.";
                return false;
            }

            try
            {
                using (var connection = GetConnection())
                {   
                    connection.Open();
                }
                return true;
            }
            catch (OleDbException ex) when (ex.Message.IndexOf("provider", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                errorMessage =
                    "Не удалось подключиться к базе данных: не найден драйвер Microsoft.ACE.OLEDB.12.0." + Environment.NewLine + Environment.NewLine +
                    "Как правило, причина одна из двух:" + Environment.NewLine +
                    "1. На компьютере не установлен Microsoft Access Database Engine." + Environment.NewLine +
                    "   Скачайте и установите его с сайта Microsoft (AccessDatabaseEngine.exe)." + Environment.NewLine +
                    "2. Разрядность установленного драйвера не совпадает с разрядностью," + Environment.NewLine +
                    "   в которой запущена программа (32-бит / 64-бит)." + Environment.NewLine +
                    "   Проверьте: Диспетчер задач -> вкладка Подробности -> у LibrarySystem.exe" + Environment.NewLine +
                    "   не должно быть пометки \"*32\", если установлен только 64-битный" + Environment.NewLine +
                    "   Access Database Engine (и наоборот — если установлен только 32-битный," + Environment.NewLine +
                    "   в свойствах проекта на вкладке Build нужно выставить Platform target = x86)." + Environment.NewLine + Environment.NewLine +
                    "Исходный текст ошибки: " + ex.Message;
                return false;
            }
            catch (Exception ex)
            {
                errorMessage = "Не удалось подключиться к базе данных." + Environment.NewLine + Environment.NewLine + "Текст ошибки: " + ex.Message;
                return false;
            }
        }
    }
}

//"Provider=Microsoft.ACE.OLEDB.16.0;" +
//            @"Data Source=C:\Users\8201116\source\repos\lab12\lab12\LibrarySystem.accdb;" +
//            @"Persist Security Info=False;");
