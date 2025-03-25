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
    public partial class OrdersForm : StyledForm
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private BindingSource _ordersBinding = new BindingSource();
        private BindingSource _itemsBinding = new BindingSource();

        public OrdersForm(IOrderService orderService, IProductService productService)
        {
            InitializeComponent();
            _orderService = orderService;
            _productService = productService;
            dgvOrders.SelectionChanged += dgvOrders_SelectionChanged;
            ConfigureGrids();
            ConfigureControls();
            this.Load += async (sender, e) => await LoadOrdersAsync();
        }

        private void ConfigureGrids()
        {
            dgvOrders.CellDoubleClick += async (sender, e) =>
            {
                if (dgvOrders.CurrentRow?.DataBoundItem is Order selectedOrder)
                {
                    using (var editForm = new OrderEditForm(_productService, selectedOrder))
                    {
                        if (editForm.ShowDialog() == DialogResult.OK)
                        {
                            await _orderService.UpdateOrderAsync(editForm.Order);
                            await LoadOrdersAsync(); // Обновляем список
                        }
                    }
                }
            };
            // Настройка грида заказов (dgvOrders)
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.Columns.Clear();
            dgvOrders.Columns.AddRange(
                new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "№", Width = 50 },
                new DataGridViewTextBoxColumn { DataPropertyName = "Order_date", HeaderText = "Дата", Width = 120 }, // Совпадение с моделью
                new DataGridViewTextBoxColumn { DataPropertyName = "Customer_name", HeaderText = "Клиент", Width = 150 }, // Совпадение с моделью
                new DataGridViewTextBoxColumn { DataPropertyName = "Status", HeaderText = "Статус", Width = 100 },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Total_amount",
                    HeaderText = "Сумма",
                    Width = 100,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
                }
            );
            dgvOrders.DataSource = _ordersBinding;

            // Настройка грида позиций (dgvItems)
            dgvItems.AutoGenerateColumns = false;
            dgvItems.Columns.Clear();
            dgvItems.Columns.AddRange(
    new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Товар" },
    new DataGridViewTextBoxColumn { DataPropertyName = "Quantity", HeaderText = "Кол-во" },
    new DataGridViewTextBoxColumn { DataPropertyName = "Unit_price", HeaderText = "Цена" },
    new DataGridViewTextBoxColumn { DataPropertyName = "TotalPrice", HeaderText = "Сумма" }
);
            dgvItems.DataSource = _itemsBinding;
        }

        private void ConfigureControls()
        {
            cmbStatus.DataSource = new List<string> { "Все", "Ожидание", "Завершено", "Отменено" };
            dtpFrom.Value = DateTime.Today.AddMonths(-1);
            dtpTo.Value = DateTime.Today;
        }

        public async Task LoadOrdersAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var orders = await _orderService.GetByDateRangeAsync(dtpFrom.Value, dtpTo.Value);

                // Отладочный вывод
                Console.WriteLine($"Загружено заказов: {orders?.Count() ?? 0}");
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        Console.WriteLine($"{order.Id}, {order.Customer_name}, {order.Order_date}");
                    }
                }

                if (orders == null || !orders.Any())
                {
                    MessageBox.Show("Нет данных для отображения.");
                    return;
                }

                if (cmbStatus.SelectedIndex > 0)
                    orders = orders.Where(o => o.Status == cmbStatus.SelectedItem.ToString()).ToList();

                _ordersBinding.DataSource = orders.ToList();
                CalculateTotal();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void CalculateTotal()
        {
            var total = _ordersBinding.List.OfType<Order>().Sum(o => o.Total_amount ?? 0);
            lblTotal.Text = $"Итого: {total:C2}";
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadOrdersAsync();
        }

        private async void btnCreateOrder_Click(object sender, EventArgs e)
        {
            using (var createForm = new OrderEditForm(_productService))
            {
                if (createForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Ожидаем асинхронную операцию создания заказа и позиций
                        await _orderService.CreateOrderWithItemsAsync(createForm.Order, createForm.OrderItems);
                        await LoadOrdersAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка создания заказа: {ex.Message}");
                    }
                }
            }
        }

        private void dgvOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow?.DataBoundItem is Order selectedOrder)
            {
                // Создаем шаблон для анонимного типа
                var templateItem = new
                {
                    ProductName = "",
                    Quantity = 0,
                    Unit_price = 0m,
                    TotalPrice = 0m
                };
                var emptyList = new[] { templateItem }.ToList();
                emptyList.Clear();

                _itemsBinding.DataSource = selectedOrder.Items?
                    .Select(i => new
                    {
                        ProductName = i.Product?.Name ?? "Неизвестно",
                        i.Quantity,
                        i.Unit_price,
                        TotalPrice = i.Quantity * i.Unit_price
                    })
                    .ToList() ?? emptyList; // Используем emptyList того же типа
            }
        }

        private async void btnCancelOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow?.DataBoundItem is Order order)
            {
                if (order.Status == "Отменено") return;

                if (MessageBox.Show("Отменить выбранный заказ?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        order.Status = "Отменено";
                        await _orderService.UpdateOrderAsync(order);
                        await LoadOrdersAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyStyles();
            ConfigureGridStyles();
        }

        private void ApplyStyles()
        {
            // Стилизация кнопок
            AppStyles.StyleButton(btnRefresh);
            AppStyles.StyleButton(btnCreateOrder);
            AppStyles.StyleButton(btnCancelOrder);

            // Стилизация выпадающих списков
            cmbStatus.Font = AppStyles.RegularFont;
            cmbStatus.BackColor = AppStyles.SecondaryColor;
            cmbStatus.ForeColor = AppStyles.PrimaryColor;

            // Стилизация DatePicker
            dtpFrom.Font = AppStyles.RegularFont;
            dtpTo.Font = AppStyles.RegularFont;

            // Стилизация меток
            AppStyles.StyleLabel(label1);
            AppStyles.StyleLabel(label2);
            AppStyles.StyleLabel(label3);
            AppStyles.StyleLabel(lblTotal);

            // Настройка цветов
            this.BackColor = AppStyles.SecondaryColor;
        }

        private void ConfigureGridStyles()
        {
            AppStyles.StyleGrid(dgvOrders);
            AppStyles.StyleGrid(dgvItems);

            // Дополнительная настройка для dgvItems
            dgvItems.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvItems.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
    }
}