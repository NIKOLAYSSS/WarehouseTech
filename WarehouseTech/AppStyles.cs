using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WarehouseTech
{
    public static class AppStyles
    {
        // Цветовая схема
        public static Color PrimaryColor { get; } = Color.FromArgb(44, 62, 80);
        public static Color SecondaryColor { get; } = Color.FromArgb(236, 240, 241);
        public static Color AccentColor { get; } = Color.FromArgb(39, 174, 96);
        public static Color ErrorColor { get; } = Color.FromArgb(231, 76, 60);

        // Шрифты
        public static Font HeaderFont { get; } = new Font("Segoe UI", 12, FontStyle.Bold);
        public static Font RegularFont { get; } = new Font("Segoe UI", 9.75f);

        // Стилизация кнопок
        public static void StyleButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = AccentColor;
            button.ForeColor = Color.White;
            button.Font = RegularFont;
            button.Padding = new Padding(5);
            button.Height = 32;
            button.AutoSize = true;
            button.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button.MinimumSize = new Size(100, 32);
        }

        // Стилизация DataGridView
        public static void StyleGrid(DataGridView grid)
        {
            grid.BorderStyle = BorderStyle.None;
            grid.EnableHeadersVisualStyles = false;
            grid.DefaultCellStyle.SelectionBackColor = SecondaryColor;
            grid.DefaultCellStyle.SelectionForeColor = PrimaryColor;
            grid.ColumnHeadersDefaultCellStyle.BackColor = PrimaryColor;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = RegularFont;
            grid.RowHeadersVisible = false;
            grid.BackgroundColor = SecondaryColor;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // Стилизация текстовых полей
        public static void StyleTextBox(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.Font = RegularFont;
            textBox.Height = 28;
        }

        public static void StyleLabel(Label label)
        {
            label.Font = RegularFont;
            label.ForeColor = PrimaryColor;
            label.AutoSize = true;
        }

        public static void StylePanel(Panel panel)
        {
            panel.BackColor = Color.White;
            panel.Padding = new Padding(20);
            panel.BorderStyle = BorderStyle.None;
        }
        public static void StyleMainForm(MainForm form)
        {
            form.BackColor = SecondaryColor;
            form.Font = RegularFont;

            // Стилизация MDI клиентской области
            foreach (Control control in form.Controls)
            {
                if (control is MdiClient mdiClient)
                {
                    mdiClient.BackColor = SecondaryColor;
                }
            }
        }
        // Обновленный StyleForm
        public static void StyleForm(Form form)
        {
            form.Font = RegularFont;
            form.BackColor = SecondaryColor;
            form.FormBorderStyle = FormBorderStyle.None;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Padding = new Padding(20);
        }
    }
}
