namespace LibrarySystem.Forms
{
    partial class BookEditForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.lblYear = new System.Windows.Forms.Label();
            this.numYear = new System.Windows.Forms.NumericUpDown();
            this.lblAddedDate = new System.Windows.Forms.Label();
            this.dtpAddedDate = new System.Windows.Forms.DateTimePicker();
            this.chkIsAvailable = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(64, 15);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Название";
            //
            // txtTitle
            //
            this.txtTitle.Location = new System.Drawing.Point(150, 15);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(250, 23);
            this.txtTitle.TabIndex = 1;
            //
            // lblAuthor
            //
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(12, 51);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(39, 15);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "Автор";
            //
            // txtAuthor
            //
            this.txtAuthor.Location = new System.Drawing.Point(150, 48);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(250, 23);
            this.txtAuthor.TabIndex = 3;
            //
            // lblYear
            //
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(12, 84);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(80, 15);
            this.lblYear.TabIndex = 4;
            this.lblYear.Text = "Год издания";
            //
            // numYear
            //
            this.numYear.Location = new System.Drawing.Point(150, 82);
            this.numYear.Maximum = new decimal(new int[] { 2100, 0, 0, 0 });
            this.numYear.Minimum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.numYear.Name = "numYear";
            this.numYear.Size = new System.Drawing.Size(100, 23);
            this.numYear.TabIndex = 5;
            this.numYear.Value = new decimal(new int[] { 2024, 0, 0, 0 });
            //
            // lblAddedDate
            //
            this.lblAddedDate.AutoSize = true;
            this.lblAddedDate.Location = new System.Drawing.Point(12, 117);
            this.lblAddedDate.Name = "lblAddedDate";
            this.lblAddedDate.Size = new System.Drawing.Size(113, 15);
            this.lblAddedDate.TabIndex = 6;
            this.lblAddedDate.Text = "Дата поступления";
            //
            // dtpAddedDate
            //
            this.dtpAddedDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAddedDate.Location = new System.Drawing.Point(150, 113);
            this.dtpAddedDate.Name = "dtpAddedDate";
            this.dtpAddedDate.Size = new System.Drawing.Size(150, 23);
            this.dtpAddedDate.TabIndex = 7;
            //
            // chkIsAvailable
            //
            this.chkIsAvailable.AutoSize = true;
            this.chkIsAvailable.Checked = true;
            this.chkIsAvailable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsAvailable.Location = new System.Drawing.Point(150, 149);
            this.chkIsAvailable.Name = "chkIsAvailable";
            this.chkIsAvailable.Size = new System.Drawing.Size(84, 19);
            this.chkIsAvailable.TabIndex = 8;
            this.chkIsAvailable.Text = "В наличии";
            this.chkIsAvailable.UseVisualStyleBackColor = true;
            //
            // btnSave
            //
            this.btnSave.Location = new System.Drawing.Point(150, 190);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 32);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(256, 190);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            //
            // errorProvider1
            //
            this.errorProvider1.ContainerControl = this;
            //
            // BookEditForm
            //
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(420, 240);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkIsAvailable);
            this.Controls.Add(this.dtpAddedDate);
            this.Controls.Add(this.lblAddedDate);
            this.Controls.Add(this.numYear);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BookEditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Книга";
            ((System.ComponentModel.ISupportInitialize)(this.numYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.NumericUpDown numYear;
        private System.Windows.Forms.Label lblAddedDate;
        private System.Windows.Forms.DateTimePicker dtpAddedDate;
        private System.Windows.Forms.CheckBox chkIsAvailable;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
