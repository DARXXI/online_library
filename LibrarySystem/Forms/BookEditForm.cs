using System;
using System.Windows.Forms;
using LibrarySystem.Models;

namespace LibrarySystem.Forms
{
    public partial class BookEditForm : Form
    {
        private readonly Book editingBook;

        public Book Book { get; private set; }

        // Режим добавления — пустая форма.
        public BookEditForm()
        {
            InitializeComponent();
            editingBook = null;
            Text = "Добавление книги";
            dtpAddedDate.Value = DateTime.Today;
        }

        // Режим редактирования — форма заполняется данными выбранной книги.
        public BookEditForm(Book book) : this()
        {
            editingBook = book;
            Text = "Редактирование книги";

            txtTitle.Text = book.Title;
            txtAuthor.Text = book.Author;
            numYear.Value = book.Year;
            dtpAddedDate.Value = book.AddedDate;
            chkIsAvailable.Checked = book.IsAvailable;
        }

        private bool Validate(out string title, out string author)
        {
            errorProvider1.Clear();
            bool isValid = true;

            title = txtTitle.Text.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                errorProvider1.SetError(txtTitle, "Укажите название книги.");
                isValid = false;
            }

            author = txtAuthor.Text.Trim();
            if (string.IsNullOrWhiteSpace(author))
            {
                errorProvider1.SetError(txtAuthor, "Укажите автора.");
                isValid = false;
            }

            return isValid;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!Validate(out string title, out string author))
            {
                return;
            }

            Book = new Book
            {
                Id = editingBook?.Id ?? 0,
                Title = title,
                Author = author,
                Year = (int)numYear.Value,
                AddedDate = dtpAddedDate.Value.Date,
                IsAvailable = chkIsAvailable.Checked,
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
