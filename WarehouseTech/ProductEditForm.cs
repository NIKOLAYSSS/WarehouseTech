using WarehouseTech.Models;
using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WarehouseTech
{
    public partial class ProductEditForm : StyledForm
    {
        public Product Product { get; private set; }

        public ProductEditForm(Product product = null)
        {
            InitializeComponent();
            Product = product ?? new Product();
            BindControls();
        }

        private void BindControls()
        {
            txtName.DataBindings.Add("Text", Product, "Name");
            txtDescription.DataBindings.Add("Text", Product, "Description");
            numPrice.DataBindings.Add("Value", Product, "Price");
            numQuantity.DataBindings.Add("Value", Product, "Quantity");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название товара");
                return false;
            }

            if (numPrice.Value <= 0)
            {
                MessageBox.Show("Цена должна быть больше нуля");
                return false;
            }

            return true;
        }
    }
}