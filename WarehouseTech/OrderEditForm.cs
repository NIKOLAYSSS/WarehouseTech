using WarehouseTech.Models;
using WarehouseTech.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using WarehouseTech.Services.Interfaces;
using System.ComponentModel;

namespace WarehouseTech
{
    public partial class OrderEditForm : StyledForm
    {
        public Order Order { get; private set; }
        public List<OrderItem> OrderItems { get; private set; } = new List<OrderItem>();

        private readonly IProductService _productService;
        private BindingList<OrderItem> _itemsBinding = new BindingList<OrderItem>();
        private List<Product> _productsCache; // Кэш товаров

        public OrderEditForm(IProductService productService, Order existingOrder = null)
        {
            InitializeComponent();
            Order = new Order
            {
                Order_date = DateTime.Now,
                Status = "Ожидание" // Статус задается здесь
            };
            txtCustomer.KeyPress += (sender, e) =>
            {
                if (e.KeyChar == (char)Keys.Enter) e.Handled = true;
            };
            _productService = productService;
            dgvItems.DataError += DgvItems_DataError; // Обработчик ошибок DataGridView
            dgvItems.CellEndEdit += DgvItems_CellEndEdit;
            ConfigureGrid();
            if (existingOrder != null)
            {
                Order = existingOrder;
                txtCustomer.Text = Order.Customer_name;
                txtCustomerContact.Text = Order.Customer_contact;
                _itemsBinding = new BindingList<OrderItem>(existingOrder.Items ?? new List<OrderItem>());
            }
            else
            {
                Order = new Order
                {
                    Order_date = DateTime.Now,
                    Status = "Ожидание"
                };
            }
        }

        private async void ConfigureGrid()
        {
            _productsCache = (await _productService.GetAllProductsAsync()).ToList();
            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Clear();

            // Колонка "Статус"
            var statusColumn = new DataGridViewComboBoxColumn
            {
                Name = "StatusColumn",
                HeaderText = "Статус",
                DataPropertyName = "Status",
                DataSource = new List<string> { "Ожидание", "Завершено", "Отменено" },
                Width = 120
            };

            // Колонка "Товар"
            var productColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "Товар",
                DataPropertyName = "Product_id",
                DataSource = _productsCache,
                DisplayMember = "Name",
                ValueMember = "Id",
                Width = 200
            };

            // Колонка "Кол-во"
            var quantityColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Кол-во",
                DataPropertyName = "Quantity",
                Width = 80
            };

            // Колонка "Цена"
            var priceColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Цена",
                DataPropertyName = "Unit_price",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
            };

            dgvItems.Columns.AddRange(statusColumn, productColumn, quantityColumn, priceColumn);
            dgvItems.DataSource = _itemsBinding;
        }

        // Обработка ошибок DataGridView
        private void DgvItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex == 0) // Колонка "Товар"
            {
                MessageBox.Show("Выберите товар из списка");
                e.ThrowException = false;
            }
        }

        private async void DgvItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                var cell = dgvItems.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
                if (cell?.Value != null && int.TryParse(cell.Value.ToString(), out int productId))
                {
                    var product = _productsCache.FirstOrDefault(p => p.Id == productId);
                    if (product != null)
                    {
                        dgvItems.Rows[e.RowIndex].Cells[2].Value = product.Price;
                    }
                }
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            _itemsBinding.Add(new OrderItem
            {
                Quantity = 1,
                Unit_price = 0
            });
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow != null && !dgvItems.CurrentRow.IsNewRow)
                _itemsBinding.RemoveAt(dgvItems.CurrentRow.Index);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                Order.Customer_name = txtCustomer.Text;
                Order.Customer_contact = txtCustomerContact.Text; // Передаем контакт
                Order.Status = dgvItems.CurrentRow.Cells["StatusColumn"].Value?.ToString() ?? "Ожидание";
                Order.Order_date = DateTime.Now; // Обновляем дату
                OrderItems = _itemsBinding.ToList();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtCustomer.Text))
            {
                MessageBox.Show("Введите имя клиента");
                return false;
            }

            foreach (var item in _itemsBinding)
            {
                if (item.Product_id == 0 || item.Quantity <= 0)
                {
                    MessageBox.Show("Заполните все товары корректно");
                    return false;
                }
            }

            return true;
        }
    }
}