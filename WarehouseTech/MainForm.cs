using WarehouseTech.Models;
using WarehouseTech.Services;
using System;
using System.Drawing;
using System.Windows.Forms;
using WarehouseTech.Services.Interfaces;
using WarehouseTech.Repositories.Interfaces;

namespace WarehouseTech
{
    public partial class MainForm : StyledForm
    {
        private readonly User _currentUser;
        private readonly IAuthService _authService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IShipmentService _shipmentService;
        private readonly IReportService _reportService;
        private readonly ISupplierService _supplierService;
        private readonly IUserRepository _userRepository;

        public MainForm(
            User user,
            IAuthService authService,
            IProductService productService,
            IOrderService orderService,
            IShipmentService shipmentService,
            IReportService reportService,
            ISupplierService supplierService,
            IUserRepository userRepository)
        {
            this.IsMdiContainer = true;
            InitializeComponent();
            _currentUser = user;
            _authService = authService;
            _productService = productService;
            _orderService = orderService;
            _shipmentService = shipmentService;
            _reportService = reportService;
            _supplierService = supplierService;
            _userRepository = userRepository;
            ConfigureForm();
            ConfigureMenuVisibility();
            UpdateStatusBar();
            AppStyles.StyleMainForm(this);
        }

        private void ConfigureForm()
        {
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;
            this.Text = $"Складская система - {_currentUser.Username}";
            this.BackColor = Color.FromArgb(240, 240, 240);
        }

        private void ConfigureMenuVisibility()
        {
            // Настройка видимости по ролям
            bool isAdmin = _currentUser.Role.Name == "Администратор";
            bool isManager = _currentUser.Role.Name == "Менеджер";
            bool isStorekeeper = _currentUser.Role.Name == "Кладовщик";

            // Меню Товары
            tsmiProducts.Visible = isAdmin || isStorekeeper;

            // Меню Поставки
            tsmiShipments.Visible = isAdmin || isStorekeeper;

            // Меню Заказы
            tsmiOrders.Visible = isAdmin || isManager;

            // Меню Отчеты
            tsmiReports.Visible = isAdmin || isManager;

            // Администрирование
            tsmiAdmin.Visible = isAdmin;
        }

        private void UpdateStatusBar()
        {
            tsslUser.Text = $"{_currentUser.Username} ({_currentUser.Role.Name})";
            tsslLastLogin.Text = _currentUser.LastLogin?.ToString("g") ?? "Нет данных";
        }

        private void OpenChildForm<T>(Func<T> formFactory) where T : Form
        {
            foreach (Form form in this.MdiChildren)
            {
                if (form.GetType() == typeof(T))
                {
                    form.Activate();
                    return;
                }
            }

            var childForm = formFactory();
            childForm.MdiParent = this;
            childForm.WindowState = FormWindowState.Maximized;
            childForm.Dock = DockStyle.Fill;
            childForm.Show();
        }

        #region Menu Handlers
        private void tsmiProducts_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new ProductsForm(_productService));
        }

        private void tsmiShipments_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new ShipmentsForm(_shipmentService, _supplierService, _productService));
        }

        private void tsmiOrders_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new OrdersForm(_orderService, _productService));
        }

        private void tsmiInventoryReport_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new ReportForm(_reportService, ReportType.Inventory));
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsmiUsers_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new UsersForm(_authService, _userRepository));
        }

        private void tsmiLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            new LoginForm(_authService).Show();
        }
        #endregion
        private void tsmiShipmentsReport_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new ReportForm(_reportService, ReportType.Shipments));
        }

        private void tsmiOrdersReport_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new ReportForm(_reportService, ReportType.Orders));
        }

        private void tsmiPopularProductsReport_Click(object sender, EventArgs e)
        {
            OpenChildForm(() => new ReportForm(_reportService, ReportType.PopularProducts));
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            StyleMenu();
            StyleStatusBar();
            this.BackColor = AppStyles.SecondaryColor;
        }

        private void StyleMenu()
        {
            menuStrip.BackColor = AppStyles.PrimaryColor;
            menuStrip.ForeColor = Color.White;
            menuStrip.Renderer = new CustomMenuRenderer();

            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                item.ForeColor = Color.White;
                item.Font = AppStyles.RegularFont;
                StyleSubMenu(item);
            }
        }

        private void StyleSubMenu(ToolStripMenuItem menuItem)
        {
            foreach (ToolStripItem subItem in menuItem.DropDownItems)
            {
                subItem.Font = AppStyles.RegularFont;
                subItem.BackColor = AppStyles.PrimaryColor;
                subItem.ForeColor = Color.White;

                if (subItem is ToolStripMenuItem subMenu)
                {
                    StyleSubMenu(subMenu);
                }
            }
        }

        private void StyleStatusBar()
        {
            statusStrip.BackColor = AppStyles.PrimaryColor;
            statusStrip.ForeColor = Color.White;

            tsslUser.ForeColor = Color.White;
            tsslLastLogin.ForeColor = Color.White;
            tsslUser.Font = AppStyles.RegularFont;
            tsslLastLogin.Font = AppStyles.RegularFont;
        }

        // Кастомный рендерер для меню
        public class CustomMenuRenderer : ToolStripProfessionalRenderer
        {
            public CustomMenuRenderer() : base(new MenuColorTable()) { }
        }

        public class MenuColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected => AppStyles.AccentColor;
            public override Color MenuItemBorder => AppStyles.AccentColor;
            public override Color MenuItemSelectedGradientBegin => AppStyles.AccentColor;
            public override Color MenuItemSelectedGradientEnd => AppStyles.AccentColor;
            public override Color MenuItemPressedGradientBegin => AppStyles.PrimaryColor;
            public override Color MenuItemPressedGradientEnd => AppStyles.PrimaryColor;
            public override Color MenuBorder => AppStyles.PrimaryColor;
            public override Color MenuStripGradientBegin => AppStyles.PrimaryColor;
            public override Color MenuStripGradientEnd => AppStyles.PrimaryColor;
        }
    }

    public enum ReportType
    {
        Inventory,
        Shipments,
        Orders,
        PopularProducts
    }
}