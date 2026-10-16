using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Filters;
using LibrarySystem.Models;
using LibrarySystem.Repositories;

namespace LibrarySystem.Forms
{
    public partial class BookListForm : Form
    {
        private readonly Repository<Book> repository = new Repository<Book>();

        public BookListForm()
        {
            InitializeComponent();
            cmbAvailability.SelectedIndex = 0;
            numYearTo.Value = DateTime.Today.Year;

            // Подписка после начальных значений, чтобы список не грузился несколько раз ещё до открытия окна.
            // Любое изменение фильтра сразу обновляет таблицу.
            txtSearch.TextChanged += Filter_Changed;
            cmbAvailability.SelectedIndexChanged += Filter_Changed;
            chkYear.CheckedChanged += ChkYear_CheckedChanged;
            numYearFrom.ValueChanged += Filter_Changed;
            numYearTo.ValueChanged += Filter_Changed;

            Load += BookListForm_Load;
        }

        private void BookListForm_Load(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void Filter_Changed(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void ChkYear_CheckedChanged(object sender, EventArgs e)
        {
            numYearFrom.Enabled = chkYear.Checked;
            numYearTo.Enabled = chkYear.Checked;
            LoadBooks();
        }

        private void BtnResetFilter_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            cmbAvailability.SelectedIndex = 0;
            chkYear.Checked = false;
        }

        // Собирает фильтр из того, что сейчас выбрано на форме.
        private BookFilter BuildFilter()
        {
            var filter = new BookFilter { Search = txtSearch.Text };

            if (cmbAvailability.SelectedIndex == 1) filter.IsAvailable = true;
            if (cmbAvailability.SelectedIndex == 2) filter.IsAvailable = false;

            if (chkYear.Checked)
            {
                filter.YearFrom = (int)numYearFrom.Value;
                filter.YearTo = (int)numYearTo.Value;
            }

            return filter;
        }

        private void LoadBooks()
        {
            try
            {
                var books = repository.Find(BuildFilter());
                dgvBooks.DataSource = books;
                ConfigureColumns();
                lblCount.Text = "Найдено: " + books.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось загрузить список книг из базы данных." + Environment.NewLine + Environment.NewLine + ex.Message,
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigureColumns()
        {
            if (dgvBooks.Columns["Id"] != null) dgvBooks.Columns["Id"].HeaderText = "ID";
            if (dgvBooks.Columns["Title"] != null) dgvBooks.Columns["Title"].HeaderText = "Название";
            if (dgvBooks.Columns["Author"] != null) dgvBooks.Columns["Author"].HeaderText = "Автор";
            if (dgvBooks.Columns["Year"] != null) dgvBooks.Columns["Year"].HeaderText = "Год";
            if (dgvBooks.Columns["AddedDate"] != null)
            {
                dgvBooks.Columns["AddedDate"].HeaderText = "Дата поступления";
                dgvBooks.Columns["AddedDate"].DefaultCellStyle.Format = "dd.MM.yyyy";
            }
            if (dgvBooks.Columns["IsAvailable"] != null) dgvBooks.Columns["IsAvailable"].HeaderText = "В наличии";
        }

        private Book GetSelectedBook()
        {
            if (dgvBooks.CurrentRow == null) return null;
            return dgvBooks.CurrentRow.DataBoundItem as Book;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new BookEditForm())
            {
                if (form.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    repository.Add(form.Book);
                    LoadBooks();
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show(
                        "Не удалось сохранить книгу." + Environment.NewLine + Environment.NewLine + (ex.InnerException?.Message ?? ex.Message),
                        "Ошибка базы данных",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedBook();
            if (selected == null)
            {
                MessageBox.Show("Выберите книгу в таблице.", "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new BookEditForm(selected))
            {
                if (form.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    repository.Update(form.Book);
                    LoadBooks();
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show(
                        "Не удалось сохранить изменения." + Environment.NewLine + Environment.NewLine + (ex.InnerException?.Message ?? ex.Message),
                        "Ошибка базы данных",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedBook();
            if (selected == null)
            {
                MessageBox.Show("Выберите книгу в таблице.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Удалить книгу «" + selected.Title + "»?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                repository.Delete(selected.Id);
                LoadBooks();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(
                    "Не удалось удалить книгу. Возможно, на неё есть ссылки в выдачах." + Environment.NewLine + Environment.NewLine + (ex.InnerException?.Message ?? ex.Message),
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
