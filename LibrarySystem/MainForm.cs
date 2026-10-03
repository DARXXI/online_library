using System;
using System.Windows.Forms;
using LibrarySystem.Forms;

namespace LibrarySystem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void BtnBooks_Click(object sender, EventArgs e)
        {
            using (var form = new BookListForm())
            {
                form.ShowDialog();
            }
        }

        private void BtnReaders_Click(object sender, EventArgs e)
        {
            using (var form = new ReaderListForm())
            {
                form.ShowDialog();
            }
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Раздел в разработке", "Отчёты", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ОПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Библиотека — система учёта" + Environment.NewLine +
                "Курсовой проект: Windows Forms + MS Access (OleDb)" + Environment.NewLine +
                "Автор: Arthur",
                "О программе",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
