using System.Windows.Forms;
namespace WarehouseTech
{
    partial class UserEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private TextBox txtUsername;
        private ComboBox cmbRole;
        private TextBox txtPassword;
        private TextBox txtConfirm;
        private Button btnSave;
        private Button btnCancel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(120, 20);
            this.txtUsername.Size = new System.Drawing.Size(200, 20);

            // cmbRole
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Location = new System.Drawing.Point(120, 60);
            this.cmbRole.Size = new System.Drawing.Size(200, 21);

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(120, 100);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(200, 20);

            // txtConfirm
            this.txtConfirm.Location = new System.Drawing.Point(120, 140);
            this.txtConfirm.PasswordChar = '*';
            this.txtConfirm.Size = new System.Drawing.Size(200, 20);

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(120, 180);
            this.btnSave.Text = "Сохранить";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(220, 180);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            // Labels
            this.label1.Text = "Логин:";
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label2.Text = "Роль:";
            this.label2.Location = new System.Drawing.Point(20, 63);
            this.label3.Text = "Пароль:";
            this.label3.Location = new System.Drawing.Point(20, 103);
            this.label4.Text = "Подтверждение:";
            this.label4.Location = new System.Drawing.Point(20, 143);

            // UserEditForm
            this.ClientSize = new System.Drawing.Size(350, 220);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.txtUsername,
                this.cmbRole,
                this.txtPassword,
                this.txtConfirm,
                this.btnSave,
                this.btnCancel,
                this.label1,
                this.label2,
                this.label3,
                this.label4
            });
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Text = "Редактирование пользователя";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}