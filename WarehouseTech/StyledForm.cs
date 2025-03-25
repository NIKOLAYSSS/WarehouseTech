using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WarehouseTech
{
    public class StyledForm : Form
    {
        public StyledForm()
        {
            this.Font = AppStyles.RegularFont;
            this.BackColor = AppStyles.SecondaryColor;
            this.Padding = new Padding(10);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Анимация для всех форм-наследников
            if (!(this is MainForm)) // Исключение для главной формы
            {
                this.Opacity = 0;
                Timer fadeIn = new Timer { Interval = 10 };
                fadeIn.Tick += (s, _) =>
                {
                    if ((this.Opacity += 0.05) >= 1)
                        fadeIn.Stop();
                };
                fadeIn.Start();
            }
        }

        private void ApplyStylesToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                if (control is Button button)
                {
                    AppStyles.StyleButton(button);
                    // Добавляем эффекты при наведении
                    button.MouseEnter += (s, e) =>
                    {
                        button.BackColor = ControlPaint.Light(AppStyles.AccentColor, 0.2f);
                    };
                    button.MouseLeave += (s, e) =>
                    {
                        button.BackColor = AppStyles.AccentColor;
                    };
                }
                else if (control is DataGridView grid) AppStyles.StyleGrid(grid);
                else if (control is TextBox textBox) AppStyles.StyleTextBox(textBox);
                else if (control is Label label) AppStyles.StyleLabel(label);
                else if (control is Panel panel) AppStyles.StylePanel(panel);

                if (control.HasChildren) ApplyStylesToControls(control.Controls);
            }
        }

    }
}
