using System;
using System.Drawing;
using System.Windows.Forms;

namespace WarehouseTech
{
    partial class OrdersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private DataGridView dgvOrders;
        private DataGridView dgvItems;
        private Button btnRefresh;
        private Button btnCreateOrder;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private ComboBox cmbStatus;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label lblTotal;
        private Button btnCancelOrder;

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
            // Инициализация компонентов
            this.dgvOrders = new DataGridView();
            this.dgvItems = new DataGridView();
            this.btnRefresh = new Button();
            this.btnCreateOrder = new Button();
            this.dtpFrom = new DateTimePicker();
            this.dtpTo = new DateTimePicker();
            this.cmbStatus = new ComboBox();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.lblTotal = new Label();
            this.btnCancelOrder = new Button();

            ((System.ComponentModel.ISupportInitialize)this.dgvOrders).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvItems).BeginInit();
            this.SuspendLayout();

            // Основной контейнер
            var mainTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Padding = new Padding(15),
                CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            };

            // Верхняя панель (фильтры + кнопки)
            var topPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                AutoSize = true,
                Margin = new Padding(0),
                Padding = new Padding(0, 5, 0, 5)
            };

            // Панель фильтров
            var filterPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                Dock = DockStyle.Fill,
                WrapContents = false,
                Margin = new Padding(0),
                Padding = new Padding(0, 3, 0, 0)
            };

            // Настройка элементов фильтрации
            this.label1 = new Label { Text = "От:", AutoSize = true };
            this.dtpFrom = new DateTimePicker
            {
                Size = new Size(135, 28),
                Margin = new Padding(0, 0, 15, 0)
            };
            this.label2 = new Label { Text = "До:", AutoSize = true };
            this.dtpTo = new DateTimePicker
            {
                Size = new Size(135, 28),
                Margin = new Padding(0, 0, 15, 0)
            };
            this.label3 = new Label { Text = "Статус:", AutoSize = true };
            this.cmbStatus = new ComboBox
            {
                Size = new Size(150, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            filterPanel.Controls.AddRange(new Control[] {
                label1, dtpFrom,
                label2, dtpTo,
                label3, cmbStatus
            });

            // Панель кнопок
            var buttonPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true,
                Dock = DockStyle.Fill,
                WrapContents = false,
                Margin = new Padding(0),
                Padding = new Padding(0, 3, 0, 0)
            };

            // Настройка кнопок
            int btnWidth = 130;
            int btnHeight = 32;

            this.btnRefresh = new Button
            {
                Text = "Обновить",
                Size = new Size(btnWidth, btnHeight),
                Margin = new Padding(10, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.btnCancelOrder = new Button
            {
                Text = "Отменить",
                Size = new Size(btnWidth, btnHeight),
                Margin = new Padding(10, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.btnCreateOrder = new Button
            {
                Text = "Новый заказ",
                Size = new Size(btnWidth, btnHeight),
                Margin = new Padding(10, 0, 0, 0),
                TextAlign = ContentAlignment.MiddleCenter
            };

            buttonPanel.Controls.AddRange(new Control[] {
                btnCreateOrder,
                btnCancelOrder,
                btnRefresh
            });

            // Настройка колонок верхней панели
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            topPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            topPanel.Controls.Add(filterPanel, 0, 0);
            topPanel.Controls.Add(buttonPanel, 1, 0);

            // Настройка таблиц данных
            var gridTable = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 1,
                Margin = new Padding(0, 10, 0, 0)
            };

            this.dgvOrders.Dock = DockStyle.Fill;
            this.dgvItems.Dock = DockStyle.Fill;

            gridTable.Controls.Add(dgvOrders, 0, 0);
            gridTable.Controls.Add(dgvItems, 0, 1);
            gridTable.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            gridTable.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));

            // Нижняя панель
            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 35,
                Padding = new Padding(5, 8, 0, 0)
            };
            this.lblTotal = new Label
            {
                Text = "Итого: 0 ₽",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.75F, FontStyle.Bold)
            };
            bottomPanel.Controls.Add(lblTotal);

            // Сборка главного контейнера
            mainTable.Controls.Add(topPanel, 0, 0);
            mainTable.Controls.Add(gridTable, 0, 1);
            mainTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            mainTable.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            this.Controls.Add(mainTable);
            this.Controls.Add(bottomPanel);

            // Настройки формы
            this.ClientSize = new Size(1280, 720);
            this.Padding = new Padding(15);
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            this.btnCancelOrder.Click += new System.EventHandler(this.btnCancelOrder_Click);

            ((System.ComponentModel.ISupportInitialize)this.dgvOrders).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.dgvItems).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}