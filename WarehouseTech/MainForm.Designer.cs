using System.Drawing;
using System.Windows.Forms;

namespace WarehouseTech
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private MenuStrip menuStrip;
        private ToolStripMenuItem tsmiProducts;
        private ToolStripMenuItem tsmiShipments;
        private ToolStripMenuItem tsmiOrders;
        private ToolStripMenuItem tsmiReports;
        private ToolStripMenuItem tsmiInventoryReport;
        private ToolStripMenuItem tsmiAdmin;
        private ToolStripMenuItem tsmiUsers;
        private ToolStripMenuItem tsmiLogout;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel tsslUser;
        private ToolStripStatusLabel tsslLastLogin;
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiProducts = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiShipments = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiReports = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiInventoryReport = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslLastLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            this.menuStrip.Font = AppStyles.RegularFont;
            this.statusStrip.Font = AppStyles.RegularFont;

            // Обновить цвета
            this.menuStrip.BackColor = AppStyles.PrimaryColor;
            this.menuStrip.ForeColor = Color.White;
            this.statusStrip.BackColor = AppStyles.PrimaryColor;

            // Добавить обработчик изменения размера
            this.SizeChanged += (s, e) =>
            {
                foreach (Form child in MdiChildren)
                {
                    child.WindowState = FormWindowState.Maximized;
                }
            };
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiProducts,
            this.tsmiShipments,
            this.tsmiOrders,
            this.tsmiReports,
            this.tsmiAdmin,
            this.tsmiLogout});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // tsmiProducts
            // 
            this.tsmiProducts.Name = "tsmiProducts";
            this.tsmiProducts.Size = new System.Drawing.Size(64, 20);
            this.tsmiProducts.Text = "Товары";
            this.tsmiProducts.Click += new System.EventHandler(this.tsmiProducts_Click);
            // 
            // tsmiShipments
            // 
            this.tsmiShipments.Name = "tsmiShipments";
            this.tsmiShipments.Size = new System.Drawing.Size(76, 20);
            this.tsmiShipments.Text = "Поставки";
            this.tsmiShipments.Click += new System.EventHandler(this.tsmiShipments_Click);
            // 
            // tsmiOrders
            // 
            this.tsmiOrders.Name = "tsmiOrders";
            this.tsmiOrders.Size = new System.Drawing.Size(62, 20);
            this.tsmiOrders.Text = "Заказы";
            this.tsmiOrders.Click += new System.EventHandler(this.tsmiOrders_Click);
            // 
            // tsmiReports
            // 
            this.tsmiReports.DropDownItems.AddRange(new ToolStripItem[] {
    new ToolStripMenuItem("Статистика по поставкам", null, tsmiShipmentsReport_Click),
    new ToolStripMenuItem("Статистика по заказам", null, tsmiOrdersReport_Click),
    new ToolStripMenuItem("Популярные товары", null, tsmiPopularProductsReport_Click)
});
            this.tsmiReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiInventoryReport});
            this.tsmiReports.Name = "tsmiReports";
            this.tsmiReports.Size = new System.Drawing.Size(64, 20);
            this.tsmiReports.Text = "Отчеты";
            // 
            // tsmiInventoryReport
            // 
            this.tsmiInventoryReport.Name = "tsmiInventoryReport";
            this.tsmiInventoryReport.Size = new System.Drawing.Size(180, 22);
            this.tsmiInventoryReport.Text = "Остатки на складе";
            this.tsmiInventoryReport.Click += new System.EventHandler(this.tsmiInventoryReport_Click);
            // 
            // tsmiAdmin
            // 
            this.tsmiAdmin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiUsers});
            this.tsmiAdmin.Name = "tsmiAdmin";
            this.tsmiAdmin.Size = new System.Drawing.Size(115, 20);
            this.tsmiAdmin.Text = "Администрирование";
            // 
            // tsmiUsers
            // 
            this.tsmiUsers.Name = "tsmiUsers";
            this.tsmiUsers.Size = new System.Drawing.Size(180, 22);
            this.tsmiUsers.Text = "Пользователи";
            this.tsmiUsers.Click += new System.EventHandler(this.tsmiUsers_Click);
            // 
            // tsmiLogout
            // 
            this.tsmiLogout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsmiLogout.Name = "tsmiLogout";
            this.tsmiLogout.Size = new System.Drawing.Size(62, 20);
            this.tsmiLogout.Text = "Выход";
            this.tsmiLogout.Click += new System.EventHandler(this.tsmiLogout_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslUser,
            this.tsslLastLogin});
            this.statusStrip.Location = new System.Drawing.Point(0, 428);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(800, 22);
            this.statusStrip.TabIndex = 1;
            // 
            // tsslUser
            // 
            this.tsslUser.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tsslUser.Name = "tsslUser";
            this.tsslUser.Size = new System.Drawing.Size(118, 17);
            this.tsslUser.Text = "Пользователь: None";
            // 
            // tsslLastLogin
            // 
            this.tsslLastLogin.Name = "tsslLastLogin";
            this.tsslLastLogin.Size = new System.Drawing.Size(160, 17);
            this.tsslLastLogin.Text = "Последний вход: Нет данных";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "Складская система";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    


        #endregion
    }
}