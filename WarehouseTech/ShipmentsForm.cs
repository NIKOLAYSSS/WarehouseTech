using WarehouseTech.Models;
using WarehouseTech.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseTech.Services.Interfaces;

namespace WarehouseTech
{
    public partial class ShipmentsForm : StyledForm
    {
        private readonly IShipmentService _shipmentService;
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;
        private BindingSource _shipmentsBinding = new BindingSource();
        private BindingSource _itemsBinding = new BindingSource();
        private BindingSource _suppliersBinding = new BindingSource(); // Новый BindingSource для поставщиков

        public ShipmentsForm(
            IShipmentService shipmentService,
            ISupplierService supplierService,
            IProductService productService)
        {
            InitializeComponent();
            dgvShipments.SelectionChanged += dgvShipments_SelectionChanged;
            _shipmentService = shipmentService;
            _supplierService = supplierService;
            _productService = productService;

            ConfigureGrids();
            ConfigureControls();
            LoadSuppliers(); // Загрузка поставщиков при инициализации формы
        }
        private void ConfigureControls()
        {
            // Пример настройки элементов управления:
            dtpFrom.Value = DateTime.Today.AddMonths(-1);
            dtpTo.Value = DateTime.Today;
        }
        private void ConfigureGrids()
        {
            // Настройка грида поставок
            dgvShipments.AutoGenerateColumns = false;
            dgvShipments.Columns.AddRange(
                new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "№", Width = 50 },
                new DataGridViewTextBoxColumn { DataPropertyName = "Shipment_date", HeaderText = "Дата", Width = 100 },
                new DataGridViewTextBoxColumn { DataPropertyName = "SupplierName", HeaderText = "Поставщик", Width = 200 },
                new DataGridViewTextBoxColumn { DataPropertyName = "Total_cost", HeaderText = "Сумма", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } }
            );
            dgvShipments.DataSource = _shipmentsBinding;

            // Настройка грида товаров поставки
            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.AddRange(
    new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Товар", Width = 200 }, // Было "Product_Name"
    new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Кол-во", Width = 80 },
    new DataGridViewTextBoxColumn { DataPropertyName = "Unit_price", HeaderText = "Цена", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } },
    new DataGridViewTextBoxColumn { DataPropertyName = "TotalPrice", HeaderText = "Сумма", Width = 100, DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" } } // Было "Total_Price"
);
            dgvItems.DataSource = _itemsBinding;

            // Настройка грида поставщиков (новый DataGridView)
            dgvSuppliers.AutoGenerateColumns = false;
            dgvSuppliers.Columns.AddRange(
                new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "№", Width = 50 },
                new DataGridViewTextBoxColumn { DataPropertyName = "Name", HeaderText = "Название", Width = 200 },
                new DataGridViewTextBoxColumn { DataPropertyName = "Contact_phone", HeaderText = "Телефон", Width = 120 },
                new DataGridViewTextBoxColumn { DataPropertyName = "Contact_email", HeaderText = "Email", Width = 150 },
                new DataGridViewTextBoxColumn { DataPropertyName = "Address", HeaderText = "Адрес", Width = 250 }
            );
            dgvSuppliers.DataSource = _suppliersBinding;
        }
        private void CalculateTotal()
        {
            if (_shipmentsBinding.List == null) return;
            var total = _shipmentsBinding.List.OfType<Shipment>().Sum(s => s.Total_cost ?? 0);
            lblTotal.Text = $"Итого: {total:C2}";
        }
        private async Task LoadSuppliers()
        {
            try
            {
                var suppliers = await _supplierService.GetAllAsync();
                _suppliersBinding.DataSource = suppliers;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки поставщиков: {ex.Message}");
            }
        }

        private async Task LoadShipmentsAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var shipments = await _shipmentService.GetByDateRangeAsync(dtpFrom.Value, dtpTo.Value);

                if (shipments == null || !shipments.Any())
                {
                    MessageBox.Show("Нет поставок за выбранный период.");
                    return;
                }

                _shipmentsBinding.DataSource = shipments;
                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void dgvShipments_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvShipments.CurrentRow?.DataBoundItem is Shipment shipment)
            {
                Console.WriteLine($"Выбрана поставка #{shipment.Id}");
                if (shipment.Items == null || !shipment.Items.Any())
                {
                    _itemsBinding.DataSource = null;
                    Console.WriteLine("Элементы поставки отсутствуют.");
                    return;
                }

                var items = shipment.Items.Select(i => new
                {
                    ProductName = i.Product?.Name ?? "Неизвестно",
                    i.Quantity,
                    i.Unit_price,
                    TotalPrice = i.Quantity * i.Unit_price
                }).ToList();

                Console.WriteLine($"Загружено элементов: {items.Count}");
                foreach (var item in items)
                {
                    Console.WriteLine($"Товар: {item.ProductName}, Цена: {item.Unit_price}, Сумма: {item.TotalPrice}");
                }

                _itemsBinding.DataSource = items;
            }
        }
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadShipmentsAsync();
            await LoadSuppliers();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new ShipmentEditForm(_supplierService, _productService))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        await _shipmentService.CreateShipmentWithItemsAsync(editForm.Shipment, editForm.ShipmentItems);
                        await LoadShipmentsAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvShipments.CurrentRow?.DataBoundItem is Shipment shipment)
            {
                if (MessageBox.Show("Удалить поставку?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        await _shipmentService.DeleteAsync(shipment.Id);
                        await LoadShipmentsAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }
            }
            // Остальные методы (btnAdd_Click, btnDelete_Click и т.д.) остаются без изменений
        }
    }
}