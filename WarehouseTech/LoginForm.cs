using System;
using System.Windows.Forms;
using WarehouseTech.Services;
using WarehouseTech.Models;
using WarehouseTech.Repositories;
using WarehouseTech.Services.Interfaces;
using System.Data.Common;

namespace WarehouseTech
{
    public partial class LoginForm : StyledForm
    {
        private readonly IAuthService _authService;
        public LoginForm(IAuthService authService)
        {
            InitializeComponent();

            // Инициализация через DI (в реальном проекте используйте контейнер)
            var dbConnection = new DatabaseConnection();
            var userRepo = new UserRepository(dbConnection);
            _authService = authService;
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var user = await _authService.LoginAsync(txtUsername.Text, txtPassword.Text);
                //ShowMainForm(user);

                if (user != null)
                {
                    ShowMainForm(user);
                }
                else
                {
                    MessageBox.Show("Неверные учетные данные", "Ошибка",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка авторизации: {ex.Message}", "Ошибка",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ShowMainForm(User user)
        {
            var dbConnection = new DatabaseConnection();

            // Инициализация репозиториев
            var productRepo = new ProductRepository(dbConnection);
            var orderRepo = new OrderRepository(dbConnection);
            var orderItemRepo = new OrderItemRepository(dbConnection);
            var supplierRepo = new SupplierRepository(dbConnection);
            var shipmentRepo = new ShipmentRepository(dbConnection);
            var userRepo = new UserRepository(dbConnection);
            var shipmentItemRepo = new ShipmentItemRepository(dbConnection);
            // Инициализация сервисов
            var productService = new ProductService(productRepo);
            var orderService = new OrderService(orderRepo, orderItemRepo);
            var supplierService = new SupplierService(supplierRepo);
            var shipmentService = new ShipmentService(shipmentRepo, shipmentItemRepo);
            var reportService = new ReportService(orderItemRepo, productRepo, shipmentRepo, orderRepo, shipmentItemRepo);

            var mainForm = new MainForm(
                user,
                _authService,
                productService,
                orderService,
                shipmentService,
                reportService,
                supplierService,
                userRepo
            );

            mainForm.Show();
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}