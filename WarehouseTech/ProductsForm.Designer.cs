using System.Drawing;
using System.Windows.Forms;

namespace WarehouseTech
{
    partial class ProductsForm
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView dgvProducts;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private TextBox txtSearch;
        private Label label1;
        private TableLayoutPanel mainLayout;
        private FlowLayoutPanel topPanel;
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
            this.dgvProducts = new DataGridView();
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.txtSearch = new TextBox();
            this.label1 = new Label();
            this.mainLayout = new TableLayoutPanel();
            this.topPanel = new FlowLayoutPanel();

            // Main Layout
            this.mainLayout.Dock = DockStyle.Fill;
            this.mainLayout.ColumnCount = 1;
            this.mainLayout.RowCount = 2;
            this.mainLayout.Padding = new Padding(20);
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            this.mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Top Panel
            this.topPanel.Dock = DockStyle.Fill;
            this.topPanel.FlowDirection = FlowDirection.LeftToRight;
            this.topPanel.AutoSize = true;
            this.topPanel.Controls.AddRange(new Control[] {
                this.label1,
                this.txtSearch,
                this.btnAdd,
                this.btnEdit,
                this.btnDelete
            });

            // Search Label
            this.label1.Text = "Поиск:";
            this.label1.Margin = new Padding(0, 5, 10, 0);
            AppStyles.StyleLabel(this.label1);

            // Search TextBox
            this.txtSearch.Size = new Size(250, 28);
            this.txtSearch.Margin = new Padding(0, 3, 20, 0);
            AppStyles.StyleTextBox(this.txtSearch);

            // Buttons
            int btnWidth = 120;
            foreach (Button btn in new[] { btnAdd, btnEdit, btnDelete })
            {
                btn.Size = new Size(btnWidth, 32);
                btn.Margin = new Padding(5, 0, 0, 0);
                AppStyles.StyleButton(btn);
            }
            this.btnAdd.Text = "Добавить";
            this.btnEdit.Text = "Изменить";
            this.btnDelete.Text = "Удалить";

            // DataGridView
            this.dgvProducts.Dock = DockStyle.Fill;
            this.dgvProducts.Margin = new Padding(0, 15, 0, 0);
            AppStyles.StyleGrid(this.dgvProducts);
            this.dgvProducts.ColumnHeadersHeight = 35;
            this.dgvProducts.RowTemplate.Height = 30;

            // Assembly
            this.mainLayout.Controls.Add(this.topPanel, 0, 0);
            this.mainLayout.Controls.Add(this.dgvProducts, 0, 1);
            this.Controls.Add(this.mainLayout);

            // Form Settings
            this.ClientSize = new Size(1000, 600);
            this.Text = "Управление товарами";
            this.Padding = new Padding(1);
            this.Load += new System.EventHandler(this.ProductsForm_Load);
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            this.txtSearch.KeyDown += new KeyEventHandler(this.txtSearch_KeyDown);
        }

        #endregion
    }
}