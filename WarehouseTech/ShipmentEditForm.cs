using WarehouseTech.Models;
using WarehouseTech.Services;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using WarehouseTech.Services.Interfaces;

namespace WarehouseTech
{
    public partial class ShipmentEditForm : StyledForm
    {
        public Shipment Shipment { get; private set; }
        public List<ShipmentItem> ShipmentItems { get; private set; } = new List<ShipmentItem>();
        private List<Product> _products = new List<Product>(); // Кэшируем список товаров

        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;
        private BindingSource _itemsBinding = new BindingSource();

        public ShipmentEditForm(ISupplierService supplierService, IProductService productService)
        {
            InitializeComponent();
            _supplierService = supplierService;
            _productService = productService;
            Shipment = new Shipment { Shipment_date = DateTime.Now };
            ConfigureControls();
        }

        private async void ConfigureControls()
        {
            try
            {
                // Загрузка поставщиков
                var suppliers = await _supplierService.GetAllAsync();
                cmbSupplier.DataSource = suppliers;
                cmbSupplier.DisplayMember = "Name";
                cmbSupplier.ValueMember = "Id";

                // Загрузка товаров
                _products = (await _productService.GetAllProductsAsync()).ToList();
                if (_products == null || !_products.Any())
                {
                    MessageBox.Show("Нет доступных товаров. Добавьте товары в систему.");
                    Close();
                    return;
                }

                // Настройка DataGridView
                dgvItems.AutoGenerateColumns = false;
                dgvItems.Columns.Clear();

                // Колонка "Товар" (ComboBox)
                var productColumn = new DataGridViewComboBoxColumn
                {
                    HeaderText = "Товар",
                    DataPropertyName = "Product_id",
                    DataSource = _products,
                    DisplayMember = "Name",
                    ValueMember = "Id",
                    Width = 200
                };
                dgvItems.Columns.Add(productColumn);

                // Колонка "Количество"
                dgvItems.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Кол-во",
                    DataPropertyName = "Quantity",
                    Width = 80
                });

                // Колонка "Цена"
                dgvItems.Columns.Add(new DataGridViewTextBoxColumn
                {
                    HeaderText = "Цена",
                    DataPropertyName = "Unit_price",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
                });

                // Привязка данных
                _itemsBinding.DataSource = new List<ShipmentItem>();
                dgvItems.DataSource = _itemsBinding;

                // Обработчик ошибок
                dgvItems.DataError += (sender, e) =>
                {
                    MessageBox.Show($"Ошибка: {e.Exception.Message}");
                    e.ThrowException = false;
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}");
                Close();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (_products == null || !_products.Any())
            {
                MessageBox.Show("Нет доступных товаров");
                return;
            }

            var newItem = new ShipmentItem
            {
                Product_id = _products.First().Id, // Значение по умолчанию
                Quantity = 1,
                Unit_price = _products.First().Price
            };
            _itemsBinding.Add(newItem);
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow != null && !dgvItems.CurrentRow.IsNewRow)
            {
                _itemsBinding.RemoveAt(dgvItems.CurrentRow.Index);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                Shipment.Supplier_id = (int)cmbSupplier.SelectedValue;
                ShipmentItems = _itemsBinding.List.OfType<ShipmentItem>().ToList();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool ValidateForm()
        {
            if (cmbSupplier.SelectedItem == null)
            {
                MessageBox.Show("Выберите поставщика");
                return false;
            }

            if (_itemsBinding.Count == 0)
            {
                MessageBox.Show("Добавьте товары в поставку");
                return false;
            }

            foreach (var item in _itemsBinding.List.OfType<ShipmentItem>())
            {
                if (item.Product_id == 0 || !_products.Any(p => p.Id == item.Product_id))
                {
                    MessageBox.Show("Выберите допустимый товар для всех позиций");
                    return false;
                }

                if (item.Quantity <= 0)
                {
                    MessageBox.Show("Количество должно быть больше 0");
                    return false;
                }

                if (item.Unit_price <= 0)
                {
                    MessageBox.Show("Цена должна быть больше 0");
                    return false;
                }
            }

            return true;
        }
    

    private async void btnAddSupplier_Click(object sender, EventArgs e)
        {
            using (var supplierForm = new SupplierEditForm())
            {
                if (supplierForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Добавляем поставщика через сервис
                        await _supplierService.AddAsync(supplierForm.Supplier);

                        // Обновляем список поставщиков
                        var suppliers = await _supplierService.GetAllAsync();
                        cmbSupplier.DataSource = suppliers;
                        cmbSupplier.SelectedValue = supplierForm.Supplier.Id;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }
            }
        }
    }
}