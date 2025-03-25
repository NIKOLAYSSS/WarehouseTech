using WarehouseTech.Models;
using System.Windows.Forms;
using System.Xml.Linq;
using System;

namespace WarehouseTech
{
    public partial class SupplierEditForm : StyledForm
    {
        public Supplier Supplier { get; private set; }

        public SupplierEditForm()
        {
            InitializeComponent();
            Supplier = new Supplier();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Настройка элементов формы
            txtName.Text = Supplier.Name;
            txtContactPhone.Text = Supplier.Contact_phone;
            txtContactEmail.Text = Supplier.Contact_email;
            txtAddress.Text = Supplier.Address;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                Supplier.Name = txtName.Text;
                Supplier.Contact_phone = txtContactPhone.Text;
                Supplier.Contact_email = txtContactEmail.Text;
                Supplier.Address = txtAddress.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Введите название поставщика");
                return false;
            }
            return true;
        }
    }
}