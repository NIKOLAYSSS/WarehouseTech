using System;
using System.Drawing;
using System.Windows.Forms;

namespace WarehouseTech
{
    partial class OrderEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvItems;
        private Button btnAddItem;
        private Button btnRemoveItem;
        private Button btnSave;
        private TextBox txtCustomer;
        private TextBox txtCustomerContact;
        private DateTimePicker dtpOrderDate;
        private Label label1;
        private Label label2;
        private Label label3;
        private TableLayoutPanel mainLayout;
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
            // Основные элементы
            this.dgvItems = new DataGridView();
            this.btnAddItem = new Button();
            this.btnRemoveItem = new Button();
            this.btnSave = new Button();
            this.txtCustomer = new TextBox();
            this.txtCustomerContact = new TextBox();
            this.dtpOrderDate = new DateTimePicker();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            // Основной контейнер
            this.mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                Padding = new Padding(20),
                ColumnStyles =
        {
            new ColumnStyle(SizeType.Absolute, 120F), // Лейблы
            new ColumnStyle(SizeType.Percent, 100F)    // Поля ввода
        },
                RowStyles =
        {
            new RowStyle(SizeType.Absolute, 40F),     // Клиент
            new RowStyle(SizeType.Absolute, 40F),     // Контакт
            new RowStyle(SizeType.Absolute, 40F),     // Дата
            new RowStyle(SizeType.Percent, 100F),     // Грид
            new RowStyle(SizeType.Absolute, 60F)      // Кнопки
        }
            };

            // Настройка главного контейнера
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.ColumnCount = 2;
            mainLayout.RowCount = 5;
            mainLayout.Padding = new Padding(20);
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            // Настройка элементов
            label1.Text = "Клиент:";
            label2.Text = "Контакт:";
            label3.Text = "Дата:";
            dtpOrderDate.Format = DateTimePickerFormat.Short;
            // Настройка DataGridView
            this.dgvItems = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = AppStyles.SecondaryColor,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 },
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = new Padding(0, 15, 0, 15)
            };
            AppStyles.StyleGrid(dgvItems);
            // Применение стилей
            AppStyles.StyleLabel(label1);
            AppStyles.StyleLabel(label2);
            AppStyles.StyleLabel(label3);
            AppStyles.StyleTextBox(txtCustomer);
            AppStyles.StyleTextBox(txtCustomerContact);
            AppStyles.StyleButton(btnAddItem);
            AppStyles.StyleButton(btnRemoveItem);
            AppStyles.StyleButton(btnSave);
            AppStyles.StyleGrid(dgvItems);

            // Настройка кнопок
            btnAddItem.Text = "➕ Добавить";
            btnRemoveItem.Text = "➖ Удалить";
            btnSave.Text = "Сохранить";

            // Размеры элементов
            int buttonWidth = 120;
            foreach (Button btn in new[] { btnAddItem, btnRemoveItem, btnSave})
            {
                btn.MinimumSize = new Size(buttonWidth, 36);
                btn.Margin = new Padding(5);
            }

            // Добавление элементов в макет
            mainLayout.Controls.Add(label1, 0, 0);
            mainLayout.Controls.Add(txtCustomer, 1, 0);
            mainLayout.Controls.Add(label2, 0, 1);
            mainLayout.Controls.Add(txtCustomerContact, 1, 1);
            mainLayout.Controls.Add(label3, 0, 2);
            mainLayout.Controls.Add(dtpOrderDate, 1, 2);
            mainLayout.Controls.Add(dgvItems, 0, 3);
            mainLayout.SetColumnSpan(dgvItems, 2);

            // Панель кнопок
            var buttonPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 10, 0, 0)
            };

            buttonPanel.Controls.AddRange(new Control[] {  btnSave, btnRemoveItem, btnAddItem });
            mainLayout.Controls.Add(buttonPanel, 0, 4);
            mainLayout.SetColumnSpan(buttonPanel, 2);

            // Настройка формы
            this.ClientSize = new Size(900, 600);
            this.MinimumSize = new Size(800, 500);
            this.Controls.Add(mainLayout);
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            //this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);


        }

        #endregion
    }
}