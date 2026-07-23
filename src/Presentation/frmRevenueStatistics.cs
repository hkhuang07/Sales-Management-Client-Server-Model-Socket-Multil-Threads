using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ElectronicsStore.Client;
using ElectronicsStore.DataTransferObject;
using ElectronicsStore.Client.Reports;
using Microsoft.Reporting.WinForms;
using static ElectronicsStore.Client.Reports.ElectronicsStoreDataSet;

namespace ElectronicsStore.Presentation
{
    public partial class frmRevenueStatistics : Form
    {
        // Remove direct dependency on OrderService, as we'll use ClientService
        // private readonly OrderService _orderService; 

        string reportsFolder = Application.StartupPath.Replace("bin\\Debug\\net8.0-windows", "Reports");
        private readonly ClientService _clientService; // Keep ClientService

        public frmRevenueStatistics(ClientService clientService)
        {
            _clientService = clientService; // Assign the injected ClientService
            InitializeComponent();

            // Remove OrderService initialization
            // _orderService = new OrderService(MapperConfig.Initialize());

            string helpURL = ConfigurationManager.AppSettings["HelpURL"]?.ToString();
            helpProvider1.HelpNamespace = helpURL + "revenuestatistics.html";
        }

        ElectronicsStoreDataSet.OrderListDataTable orderListDataTable = new ElectronicsStoreDataSet.OrderListDataTable();
        // List<OrderDTO> OrderList = new List<OrderDTO>(); // This field is not actively used for data, can be removed if desired.

        /// <summary>
        /// Loads order data from the server and populates the report viewer.
        /// </summary>
        /// <param name="startDate">Optional start date for filtering.</param>
        /// <param name="endDate">Optional end date for filtering.</param>
        private async Task LoadOrderReportData(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                List<OrderDTO> orderList;
                string reportDescription;

                // Determine if filtering by date range or showing all orders
                if (startDate.HasValue && endDate.HasValue)
                {
                    // If you have a server-side method for date range, use it:
                    // orderList = await _clientService.GetOrdersByDateRangeAsync(startDate.Value, endDate.Value);
                    // For now, we'll get all and filter client-side, assuming GetAllOrdersAsync pulls all necessary data.
                    orderList = await _clientService.GetAllOrdersAsync();
                    orderList = orderList.Where(o => o.Date >= startDate.Value && o.Date <= endDate.Value).ToList();

                    reportDescription = $"From Date: {startDate.Value.Date:MM/dd/yyyy} - To Date: {endDate.Value.Date:MM/dd/yyyy}";
                }
                else
                {
                    orderList = await _clientService.GetAllOrdersAsync(); // Get all orders if no date range is specified
                    reportDescription = "(All Time)";
                }

                // Clear existing data and populate the DataTable
                orderListDataTable.Clear();
                foreach (var row in orderList)
                {
                    orderListDataTable.AddOrderListRow(row.ID,
                        row.EmployeeID,
                        row.EmployeeName,
                        row.CustomerID,
                        row.CustomerName,
                        row.Date,
                        row.Note,
                        (double)row.TotalPrice); // Explicitly cast 'decimal' to 'double' to resolve the error
                }

                // Setup the ReportViewer
                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "OrderList"; // Must match the name in your .rdlc file
                reportDataSource.Value = orderListDataTable;

                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource);
                reportViewer.LocalReport.ReportPath = Path.Combine(reportsFolder, "rptRevenueStatistics.rdlc");

                // Set the report parameter for description
                ReportParameter reportParameter = new ReportParameter("ResultDescription", reportDescription);
                reportViewer.LocalReport.SetParameters(reportParameter);

                // Configure report viewer display
                reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer.ZoomMode = ZoomMode.Percent;
                reportViewer.ZoomPercent = 100;
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading revenue statistics data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void frmRevenueStatistics_Load(object sender, EventArgs e) // Renamed from rptRevenueStatistics_Load
        {
            await LoadOrderReportData(null, null); // Load all orders initially
        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            // Pass the selected dates to the LoadOrderReportData method
            await LoadOrderReportData(dtpStart.Value.Date, dtpEnd.Value.Date);
        }

        private async void btnShowAll_Click(object sender, EventArgs e)
        {
            // Call LoadOrderReportData without specific dates to show all orders
            await LoadOrderReportData(null, null);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //Check if have no data in ReportViewer
                if (reportViewer.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("No report data to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                byte[] bytes = reportViewer.LocalReport.Render(
                    "PDF", null,
                    out string newMimeType,
                    out string newEncoding,
                    out string newExtension,
                    out string[] newStreamIds,
                    out Warning[] newWarnings
                    );
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PDF Files(*.pdf)|*.pdf";
                    saveFileDialog.DefaultExt = "pdf";
                    saveFileDialog.FileName = "Revenue_Statistics_" + DateTime.Now.Date.ToString("dd_MM_yyyy") + ".pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Save File PDF
                        File.WriteAllBytes(saveFileDialog.FileName, bytes);
                        MessageBox.Show("Revenue statistics saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when printing report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Event handler, no specific logic here.
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Use Close() to ensure proper form closure
        }

        private void frmRevenueStatistics_Load_1(object sender, EventArgs e)
        {

        }
    }
}