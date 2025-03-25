using WarehouseTech.Models;
using WarehouseTech.Services;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseTech.Services.Interfaces;

namespace WarehouseTech
{
    public partial class ProductsForm : StyledForm
    {
        private readonly IProductService _productService;
        private BindingSource _bindingSource = new BindingSource();
        private Product _selectedProduct => _bindingSource.Current as Product;

        public ProductsForm(IProductService productService)
        {
            InitializeComponent();
            _productService = productService;
            ConfigureGrid();
            //ConfigurePermissions();
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
        }

        private void ConfigurePermissions()
        {
            bool canEdit = Program.CurrentUser.Role.Name == "Администратор" ||
                          Program.CurrentUser.Role.Name == "Кладовщик";

            btnAdd.Enabled = canEdit;
            btnEdit.Enabled = canEdit;
            btnDelete.Enabled = canEdit;
        }
        // В класс ProductsForm добавить обработчик нажатия клавиш
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplySearchFilter();
                e.Handled = true; // Предотвращаем звуковой сигнал в Windows
            }
        }

        // Обновленный метод для применения фильтра
        private void ApplySearchFilter()
        {
            try
            {
                if (_bindingSource.DataSource == null) return;

                string searchText = txtSearch.Text.Trim();
                searchText = searchText.Replace("'", "''");

                if (string.IsNullOrEmpty(searchText))
                {
                    _bindingSource.Filter = string.Empty;
                }
                else
                {
                    _bindingSource.Filter = $@"
                [Name] LIKE '%{searchText}%' OR 
                [Description] LIKE '%{searchText}%'
            ";
                }

                // Принудительное обновление DataGridView
                dgvProducts.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка фильтрации: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void ConfigureGrid()
        {
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.Columns.AddRange(
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Id",
                    HeaderText = "ID",
                    Width = 50
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Name",
                    HeaderText = "Наименование",
                    Width = 200
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Description",
                    HeaderText = "Описание",
                    Width = 300
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Price",
                    HeaderText = "Цена",
                    Width = 80,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "C2" }
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Quantity",
                    HeaderText = "Остаток",
                    Width = 80
                }
            );

            dgvProducts.DataSource = _bindingSource;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        public async Task LoadDataAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var products = await _productService.GetAllProductsAsync();
                _bindingSource.DataSource = products.ToList();
                _bindingSource.ResetBindings(false); // Важно!
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new ProductEditForm())
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    await _productService.AddProductAsync(editForm.Product);
                    await LoadDataAsync();
                }
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedProduct == null) return;

            using (var editForm = new ProductEditForm(_selectedProduct))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    await _productService.UpdateProductAsync(editForm.Product);
                    await LoadDataAsync();
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedProduct == null) return;

            if (MessageBox.Show("Удалить выбранный товар?",
                "Подтверждение",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                await _productService.DeleteProductAsync(_selectedProduct.Id);
                await LoadDataAsync();
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Для мгновенной фильтрации при изменении текста
            ApplySearchFilter();
        }

        private async void ProductsForm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();
        }
    }
}