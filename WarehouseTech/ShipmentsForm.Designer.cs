using System.Windows.Forms;

namespace WarehouseTech
{
    partial class ShipmentsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private DataGridView dgvShipments;
        private DataGridView dgvItems;
        private Button btnRefresh;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private Label label1;
        private Label label2;
        private Label lblTotal;
        private DataGridView dgvSuppliers;
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
            this.dgvSuppliers = new System.Windows.Forms.DataGridView();
            this.dgvShipments = new System.Windows.Forms.DataGridView();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            this.dgvSuppliers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSuppliers.Location = new System.Drawing.Point(12, 420);
            this.dgvSuppliers.Size = new System.Drawing.Size(800, 150);
            this.dgvSuppliers.Name = "dgvSuppliers";

            // Добавьте dgvSuppliers в Controls:
            this.Controls.Add(this.dgvSuppliers);
            // dgvShipments
            this.dgvShipments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvShipments.Location = new System.Drawing.Point(12, 40);
            this.dgvShipments.Size = new System.Drawing.Size(800, 200);

            // dgvItems
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvItems.Location = new System.Drawing.Point(12, 260);
            this.dgvItems.Size = new System.Drawing.Size(800, 150);

            // btnRefresh
            this.btnRefresh.Location = new System.Drawing.Point(12, 10);
            this.btnRefresh.Text = "Обновить";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);

            // btnAdd
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(600, 10);
            this.btnAdd.Text = "Добавить";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);

            // btnEdit
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(700, 10);
            this.btnEdit.Text = "Изменить";

            // btnDelete
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(800, 10);
            this.btnDelete.Text = "Удалить";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // dtpFrom
            this.dtpFrom.Location = new System.Drawing.Point(200, 12);

            // dtpTo
            this.dtpTo.Location = new System.Drawing.Point(350, 12);

            // Labels
            this.label1.Text = "От:";
            this.label1.Location = new System.Drawing.Point(150, 15);
            this.label2.Text = "До:";
            this.label2.Location = new System.Drawing.Point(320, 15);

            // lblTotal
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.Location = new System.Drawing.Point(12, 420);
            this.lblTotal.AutoSize = true;

            // ShipmentsForm
            this.ClientSize = new System.Drawing.Size(900, 450);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.dgvShipments,
                this.dgvItems,
                this.btnRefresh,
                this.btnAdd,
                this.btnEdit,
                this.btnDelete,
                this.dtpFrom,
                this.dtpTo,
                this.label1,
                this.label2,
                this.lblTotal
            });
            this.Text = "Управление поставками";
            ((System.ComponentModel.ISupportInitialize)(this.dgvShipments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}