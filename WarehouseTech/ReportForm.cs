using WarehouseTech.Models;
using WarehouseTech.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WarehouseTech.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;

namespace WarehouseTech
{
    public partial class ReportForm : StyledForm
    {
        private readonly IReportService _reportService;
        private readonly ReportType _reportType;
        private BindingSource _bindingSource = new BindingSource();

        public ReportForm(IReportService reportService, ReportType reportType)
        {
            InitializeComponent();
            _reportService = reportService;
            _reportType = reportType;
            ConfigureForm();
        }

        private void ConfigureForm()
        {
            Text = GetReportTitle();
            ConfigureGrid();
            SetDateFilterVisibility();
        }

        private string GetReportTitle()
        {
            switch (_reportType)
            {
                case ReportType.Inventory:
                    return "Отчет по остаткам товаров";
                case ReportType.Shipments:
                    return "Статистика по поставкам";
                case ReportType.Orders:
                    return "Статистика по заказам";
                case ReportType.PopularProducts:
                    return "Популярные товары";
                default:
                    return "Отчет";
            }
        }

        private void ConfigureGrid()
        {
            dgvReport.AutoGenerateColumns = false;
            dgvReport.Columns.Clear();

            switch (_reportType)
            {
                case ReportType.Inventory:
                    ConfigureInventoryColumns();
                    break;
                case ReportType.Shipments:
                    ConfigureShipmentsColumns();
                    break;
                case ReportType.Orders:
                    ConfigureOrdersColumns();
                    break;
                case ReportType.PopularProducts:
                    ConfigurePopularProductsColumns();
                    break;
            }

            dgvReport.DataSource = _bindingSource;
        }

        private void ConfigureInventoryColumns()
        {
            dgvReport.Columns.AddRange(
                new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Товар", Width = 200 },
                new DataGridViewTextBoxColumn { DataPropertyName = "CurrentPrice", HeaderText = "Цена", Width = 100 },
                new DataGridViewTextBoxColumn { DataPropertyName = "CurrentQuantity", HeaderText = "Остаток", Width = 80 },
                new DataGridViewTextBoxColumn { DataPropertyName = "TotalSupplied", HeaderText = "Поставлено", Width = 80 },
                new DataGridViewTextBoxColumn { DataPropertyName = "TotalSold", HeaderText = "Продано", Width = 80 }
            );
        }

        private void ConfigureShipmentsColumns()
        {
            dgvReport.Columns.AddRange(
                new DataGridViewTextBoxColumn { DataPropertyName = "ShipmentDate", HeaderText = "Дата" },
                new DataGridViewTextBoxColumn { DataPropertyName = "SupplierName", HeaderText = "Поставщик" },
                new DataGridViewTextBoxColumn { DataPropertyName = "TotalQuantity", HeaderText = "Количество" },
                new DataGridViewTextBoxColumn { DataPropertyName = "TotalCost", HeaderText = "Сумма" }
            );
        }

        private void ConfigureOrdersColumns()
        {
            dgvReport.Columns.AddRange(
                new DataGridViewTextBoxColumn { DataPropertyName = "OrderDate", HeaderText = "Дата", Width = 100 },
                new DataGridViewTextBoxColumn { DataPropertyName = "CustomerName", HeaderText = "Клиент", Width = 150 },
                new DataGridViewTextBoxColumn { DataPropertyName = "TotalProductsTypes", HeaderText = "Товаров", Width = 80 },
                new DataGridViewTextBoxColumn { DataPropertyName = "TotalQuantity", HeaderText = "Количество", Width = 80 },
                new DataGridViewTextBoxColumn { DataPropertyName = "TotalAmount", HeaderText = "Сумма", Width = 100 }
            );
        }

        private void ConfigurePopularProductsColumns()
        {
            dgvReport.Columns.AddRange(
                new DataGridViewTextBoxColumn { DataPropertyName = "ProductName", HeaderText = "Товар", Width = 200 },
                new DataGridViewTextBoxColumn { DataPropertyName = "TotalOrders", HeaderText = "Заказов", Width = 80 },
                new DataGridViewTextBoxColumn { DataPropertyName = "TotalSoldQuantity", HeaderText = "Продано", Width = 80 },
                new DataGridViewTextBoxColumn { DataPropertyName = "PopularityRank", HeaderText = "Рейтинг", Width = 80 }
            );
        }

        private void SetDateFilterVisibility()
        {
            // Показывать фильтры для всех отчетов, включая Inventory
            bool showDateFilter = true;

            lblFrom.Visible = showDateFilter;
            lblTo.Visible = showDateFilter;
            dtpFrom.Visible = showDateFilter;
            dtpTo.Visible = showDateFilter;
            btnApplyFilter.Visible = showDateFilter;
        }

        private async Task LoadReportDataAsync()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IEnumerable<object> reportData = null;
                DateTime startDate = dtpFrom.Value.Date;
                DateTime endDate = dtpTo.Value.Date.AddDays(1).AddSeconds(-1);

                switch (_reportType)
                {
                    case ReportType.Inventory:
                        reportData = await _reportService.GetInventoryReportAsync();
                        break;
                    case ReportType.Shipments:
                        reportData = await _reportService.GetShipmentStatisticsAsync(startDate, endDate);
                        break;
                    case ReportType.Orders:
                        reportData = await _reportService.GetOrderStatisticsAsync(startDate, endDate);
                        break;
                    case ReportType.PopularProducts:
                        reportData = await _reportService.GetPopularProductsReportAsync();
                        break;
                }

                if (reportData == null)
                {
                    MessageBox.Show("Нет данных для отображения.");
                    return;
                }

                _bindingSource.DataSource = reportData.ToList(); // Явное преобразование в список
                dgvReport.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void ReportForm_Load(object sender, EventArgs e)
        {
            await LoadReportDataAsync();
            dgvReport.Refresh(); // Принудительное обновление грида
        }

        private async void btnApplyFilter_Click(object sender, EventArgs e)
        {
            await LoadReportDataAsync();
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Экспорт отчета"
            })
            {
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (var workbook = new XLWorkbook())
                        {
                            var worksheet = workbook.Worksheets.Add("Отчет");

                            // Заголовки
                            for (int i = 0; i < dgvReport.Columns.Count; i++)
                            {
                                worksheet.Cell(1, i + 1).Value = dgvReport.Columns[i].HeaderText;
                            }

                            // Данные
                            for (int row = 0; row < dgvReport.Rows.Count; row++)
                            {
                                for (int col = 0; col < dgvReport.Columns.Count; col++)
                                {
                                    worksheet.Cell(row + 2, col + 1).Value =
                                        dgvReport.Rows[row].Cells[col].Value?.ToString();
                                }
                            }

                            workbook.SaveAs(saveDialog.FileName);
                            MessageBox.Show("Экспорт завершен успешно!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}");
                    }
                }
            }
        }
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadReportDataAsync();
        }
    }

    
}