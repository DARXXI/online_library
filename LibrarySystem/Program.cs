using System;
using System.Windows.Forms;
using LibrarySystem.Data;

namespace LibrarySystem
{
    internal static class Program
    {
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

            Application.Run(new MainForm());
        }
    }
}
