using System;
using System.Windows.Forms;
using LibrarySystem.Models;

namespace LibrarySystem.Forms
{
    public partial class ReaderEditForm : Form
    {
        private readonly Reader editingReader;

        public Reader Reader { get; private set; }

        // Режим добавления — пустая форма.
        public ReaderEditForm()
        {
            InitializeComponent();
            editingReader = null;
            Text = "Добавление читателя";
            dtpRegisteredDate.Value = DateTime.Today;
        }

        // Режим редактирования — форма заполняется данными выбранного читателя.
        public ReaderEditForm(Reader reader) : this()
        {
            editingReader = reader;
            Text = "Редактирование читателя";

            txtFullName.Text = reader.FullName;
            txtPhone.Text = reader.Phone;
            txtEmail.Text = reader.Email;
            dtpRegisteredDate.Value = reader.RegisteredDate;
        }

        private bool Validate(out string fullName, out string email)
        {
            errorProvider1.Clear();
            bool isValid = true;

            fullName = txtFullName.Text.Trim();
            if (string.IsNullOrWhiteSpace(fullName))
            {
                errorProvider1.SetError(txtFullName, "Укажите ФИО читателя.");
                isValid = false;
            }

            email = txtEmail.Text.Trim();
            if (!string.IsNullOrEmpty(email) && !email.Contains("@"))
            {
                errorProvider1.SetError(txtEmail, "Некорректный email.");
                isValid = false;
            }

            return isValid;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!Validate(out string fullName, out string email))
            {
                return;
            }

            Reader = new Reader
            {
                Id = editingReader?.Id ?? 0,
                FullName = fullName,
                Phone = txtPhone.Text.Trim(),
                Email = email,
                RegisteredDate = dtpRegisteredDate.Value.Date,
            };

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
