using System;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using LibrarySystem.Models;
using LibrarySystem.Repositories;

namespace LibrarySystem.Forms
{
    public partial class ReaderListForm : Form
    {
        private readonly Repository<Reader> repository = new Repository<Reader>();

        public ReaderListForm()
        {
            InitializeComponent();
            Load += ReaderListForm_Load;
        }

        private void ReaderListForm_Load(object sender, EventArgs e)
        {
            LoadReaders();
        }

        private void LoadReaders()
        {
            try
            {
                dgvReaders.DataSource = repository.GetAll().OrderBy(r => r.FullName).ToList();
                ConfigureColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось загрузить список читателей из базы данных." + Environment.NewLine + Environment.NewLine + ex.Message,
                    "Ошибка базы данных",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigureColumns()
        {
            if (dgvReaders.Columns["Id"] != null) dgvReaders.Columns["Id"].HeaderText = "ID";
            if (dgvReaders.Columns["FullName"] != null) dgvReaders.Columns["FullName"].HeaderText = "ФИО";
            if (dgvReaders.Columns["Phone"] != null) dgvReaders.Columns["Phone"].HeaderText = "Телефон";
            if (dgvReaders.Columns["Email"] != null) dgvReaders.Columns["Email"].HeaderText = "Email";
            if (dgvReaders.Columns["RegisteredDate"] != null)
            {
                dgvReaders.Columns["RegisteredDate"].HeaderText = "Дата регистрации";
                dgvReaders.Columns["RegisteredDate"].DefaultCellStyle.Format = "dd.MM.yyyy";
            }
        }

        private Reader GetSelectedReader()
        {
            if (dgvReaders.CurrentRow == null) return null;
            return dgvReaders.CurrentRow.DataBoundItem as Reader;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            using (var form = new ReaderEditForm())
            {
                if (form.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    repository.Add(form.Reader);
                    LoadReaders();
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show(
                        "Не удалось сохранить читателя." + Environment.NewLine + Environment.NewLine + (ex.InnerException?.Message ?? ex.Message),
                        "Ошибка базы данных",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            var selected = GetSelectedReader();
            if (selected == null)
            {
                MessageBox.Show("Выберите читателя в таблице.", "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var form = new ReaderEditForm(selected))
            {
                if (form.ShowDialog(this) != DialogResult.OK) return;

                try
                {
                    repository.Update(form.Reader);
                    LoadReaders();
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
            var selected = GetSelectedReader();
            if (selected == null)
            {
                MessageBox.Show("Выберите читателя в таблице.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                "Удалить читателя «" + selected.FullName + "»?",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                repository.Delete(selected.Id);
                LoadReaders();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show(
                    "Не удалось удалить читателя. Возможно, на него есть ссылки в выдачах." + Environment.NewLine + Environment.NewLine + (ex.InnerException?.Message ?? ex.Message),
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
