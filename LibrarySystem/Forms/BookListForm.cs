using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
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
            Load += BookListForm_Load;
        }

        private void BookListForm_Load(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void LoadBooks()
        {
            try
            {
                dgvBooks.DataSource = repository.GetAll().OrderBy(b => b.Title).ToList();
                ConfigureColumns();
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
