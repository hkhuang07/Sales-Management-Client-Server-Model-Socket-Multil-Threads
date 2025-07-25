using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectronicsStore.Client;
using ElectronicsStore.DataTransferObject;
using ElectronicsStore.Client.Reports;
using Microsoft.Reporting.WinForms;

namespace ElectronicsStore.Presentation
{
    public partial class frmProductStatistics : Form
    {
        string reportsFolder = Application.StartupPath.Replace("bin\\Debug\\net8.0-windows", "Reports");
        private readonly ClientService _clientService;

        public frmProductStatistics(ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
            //_clientService = clientService;
            string helpURL = ConfigurationManager.AppSettings["HelpURL"]?.ToString();
            helpProvider1.HelpNamespace = helpURL + "productstatistics.html";
        }

        ElectronicsStoreDataSet.ProductListDataTable productListDataTable = new ElectronicsStoreDataSet.ProductListDataTable();

        public async Task LoadData()
        {
            try
            {
                // Get Categories and Manufacturers from the server via ClientService
                var categories = await _clientService.GetAllCategoriesAsync();
                cboCategory.DataSource = categories;
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "ID";
                cboCategory.SelectedIndex = -1; // Set no initial selection

                var manufacturers = await _clientService.GetAllManufacturersAsync();
                cboManufacturer.DataSource = manufacturers;
                cboManufacturer.DisplayMember = "ManufacturerName";
                cboManufacturer.ValueMember = "ID";
                cboManufacturer.SelectedIndex = -1; // Set no initial selection
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading category and manufacturer data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void frmProductStatistics_Load(object sender, EventArgs e)
        {
            await LoadData(); // Wait for LoadData to complete

            await LoadProductReportData(null, null); // Load all products initially
        }

        private async Task LoadProductReportData(int? manufacturerId, int? categoryId)
        {
            List<ProductDTO> productList;
            string reportDescription;

            try
            {
                if (manufacturerId == null && categoryId == null)
                {
                    productList = await _clientService.GetAllProductsAsync();
                    reportDescription = "(All Products)";
                }
                else
                {
                    productList = await _clientService.GetAllProductsAsync(); // Get all and filter client-side

                    if (manufacturerId.HasValue)
                    {
                        productList = productList.Where(p => p.ManufacturerID == manufacturerId.Value).ToList();
                    }
                    if (categoryId.HasValue)
                    {
                        productList = productList.Where(p => p.CategoryID == categoryId.Value).ToList();
                    }

                    string manufacturerName = manufacturerId.HasValue ? cboManufacturer.Text : "";
                    string categoryName = categoryId.HasValue ? cboCategory.Text : "";

                    reportDescription = "(";
                    if (manufacturerId.HasValue)
                    {
                        reportDescription += $"Manufacturer: {manufacturerName}";
                    }
                    if (categoryId.HasValue)
                    {
                        if (manufacturerId.HasValue) reportDescription += " - ";
                        reportDescription += $"Category: {categoryName}";
                    }
                    reportDescription += ")";
                }

                productListDataTable.Clear();
                foreach (var row in productList)
                {
                    // Ensure ProductListDataTable has corresponding columns
                    productListDataTable.AddProductListRow(row.ID,
                        row.ManufacturerID,
                        row.ManufacturerName,
                        row.CategoryID,
                        row.CategoryName,
                        row.ProductName,
                        row.Price,
                        row.Quantity,
                        row.Image,
                        row.Description);
                }

                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "ProductList";
                reportDataSource.Value = productListDataTable;
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(reportDataSource);
                reportViewer.LocalReport.ReportPath = Path.Combine(reportsFolder, "rptProductStatistics.rdlc");

                ReportParameter reportParameter = new ReportParameter("ResultDescription", reportDescription);
                reportViewer.LocalReport.SetParameters(reportParameter);

                reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer.ZoomMode = ZoomMode.Percent;
                reportViewer.ZoomPercent = 100;
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void btnFilter_Click(object sender, EventArgs e)
        {
            int? selectedManufacturerId = null;
            if (cboManufacturer.SelectedValue != null && cboManufacturer.SelectedValue != DBNull.Value)
            {
                selectedManufacturerId = Convert.ToInt32(cboManufacturer.SelectedValue);
            }

            int? selectedCategoryId = null;
            if (cboCategory.SelectedValue != null && cboCategory.SelectedValue != DBNull.Value)
            {
                selectedCategoryId = Convert.ToInt32(cboCategory.SelectedValue);
            }

            await LoadProductReportData(selectedManufacturerId, selectedCategoryId);
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
                    saveFileDialog.FileName = "Product_Statistics_" + DateTime.Now.Date.ToString("dd_MM_yyyy") + ".pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Save File PDF
                        File.WriteAllBytes(saveFileDialog.FileName, bytes);
                        MessageBox.Show("Product statistics saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            // No specific logic here
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); // Close the form
        }
    }
}