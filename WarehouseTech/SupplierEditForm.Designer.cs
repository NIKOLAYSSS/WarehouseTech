using System.Windows.Forms;

namespace WarehouseTech
{
    partial class SupplierEditForm
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtName;
        private TextBox txtContactPhone;
        private TextBox txtContactEmail;
        private TextBox txtAddress;
        private Button btnSave;
        private Button btnCancel;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        /// <summary>
        /// Required designer variable.
        /// </summary>


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
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtContactPhone = new System.Windows.Forms.TextBox();
            this.txtContactEmail = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 20);
            this.txtName.Size = new System.Drawing.Size(250, 20);
            this.txtName.TabIndex = 0;

            // 
            // txtContactPhone
            // 
            this.txtContactPhone.Location = new System.Drawing.Point(120, 60);
            this.txtContactPhone.Size = new System.Drawing.Size(250, 20);
            this.txtContactPhone.TabIndex = 1;

            // 
            // txtContactEmail
            // 
            this.txtContactEmail.Location = new System.Drawing.Point(120, 100);
            this.txtContactEmail.Size = new System.Drawing.Size(250, 20);
            this.txtContactEmail.TabIndex = 2;

            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(120, 140);
            this.txtAddress.Multiline = true;
            this.txtAddress.Size = new System.Drawing.Size(250, 60);
            this.txtAddress.TabIndex = 3;

            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(200, 220);
            this.btnSave.Size = new System.Drawing.Size(80, 25);
            this.btnSave.Text = "Сохранить";
            this.btnSave.TabIndex = 4;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(290, 220);
            this.btnCancel.Size = new System.Drawing.Size(80, 25);
            this.btnCancel.Text = "Отмена";
            this.btnCancel.TabIndex = 5;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            // 
            // label1
            // 
            this.label1.Text = "Название:";
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.AutoSize = true;

            // 
            // label2
            // 
            this.label2.Text = "Телефон:";
            this.label2.Location = new System.Drawing.Point(20, 63);
            this.label2.AutoSize = true;

            // 
            // label3
            // 
            this.label3.Text = "Email:";
            this.label3.Location = new System.Drawing.Point(20, 103);
            this.label3.AutoSize = true;

            // 
            // label4
            // 
            this.label4.Text = "Адрес:";
            this.label4.Location = new System.Drawing.Point(20, 143);
            this.label4.AutoSize = true;

            // 
            // SupplierEditForm
            // 
            this.ClientSize = new System.Drawing.Size(400, 260);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Редактирование поставщика";
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.txtName,
                this.txtContactPhone,
                this.txtContactEmail,
                this.txtAddress,
                this.btnSave,
                this.btnCancel,
                this.label1,
                this.label2,
                this.label3,
                this.label4
            });
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}