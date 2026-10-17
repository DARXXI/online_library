using LibrarySystem;
using LibrarySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibrarySystem.Data;

namespace LibrarySystem
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!DbHelper.TryEnsureDatabaseReady(out string errorMessage))
            {
                MessageBox.Show(errorMessage, "Ошибка базы данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                //DatabaseContext.CreateTables();
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка базы данных:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
