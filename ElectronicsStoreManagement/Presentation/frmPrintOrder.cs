using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectronicsStore.Client.Reports;
using Microsoft.Reporting.WinForms;
using ElectronicsStore.Client; // Add this
using ElectronicsStore.DataTransferObject; // Add this
using System.IO; // Required for Path.Combine

namespace ElectronicsStore.Presentation
{
    public partial class frmPrintOrder : Form
    {
        // Vẫn giữ ElectronicsStoreDataSet.OrderDetailsListDataTable nếu bạn đang sử dụng strongly-typed DataSet cho báo cáo.
        // Nếu không, bạn có thể tạo một List<OrderDetailsDTO> và truyền vào ReportDataSource.
        // Giả định bạn vẫn dùng DataSet cho ReportViewer.
        ElectronicsStoreDataSet.OrderDetailsListDataTable orderDetailsListDataTable = new ElectronicsStoreDataSet.OrderDetailsListDataTable();
        string reportsFolder = Application.StartupPath.Replace("bin\\Debug\\net8.0-windows", "Reports");
        int id; // Mã hóa đơn

        // Thay thế OrderService và OrderDetailsService bằng ClientService
        private readonly ClientService _clientService;

        public frmPrintOrder(int orderID, ClientService clientService) // Thêm tham số ClientService
        {
            InitializeComponent();
            id = orderID;
            _clientService = clientService; // Gán ClientService được truyền vào

            string helpURL = ConfigurationManager.AppSettings["HelpURL"]!.ToString();
            helpProvider1.HelpNamespace = helpURL + "printorder.html";
        }

        private async void frmPrintOrder_Load(object sender, EventArgs e) // Made async
        {
            OrderDTO orderDto = null;
            List<OrderDetailsDTO> detailsDto = null;

            try
            {
                // 1. Lấy thông tin đơn hàng từ ClientService
                orderDto = await _clientService.GetOrderByIdAsync(id); // Call async method

                if (orderDto != null)
                {
                    // 2. Lấy danh sách chi tiết đơn hàng từ ClientService
                    detailsDto = await _clientService.GetOrderDetailsByOrderIdAsync(id); // Call async method

                    // 3. Chuyển dữ liệu chi tiết sang định dạng DataTable để truyền vào ReportViewer
                    orderDetailsListDataTable.Clear();

                    foreach (var detail in detailsDto)
                    {
                        orderDetailsListDataTable.AddOrderDetailsListRow(
                            id, // OrderID
                            detail.ProductID, // ProductID
                            detail.ProductName ?? string.Empty, // ProductName
                            detail.Quantity, // Quantity
                            detail.Price, // Price
                            detail.Quantity * detail.Price // TotalPrice
                        );
                    }

                    // 4. Đổ dữ liệu vào ReportViewer
                    ReportDataSource reportDataSource = new ReportDataSource
                    {
                        Name = "OrderDetailsList",
                        Value = orderDetailsListDataTable
                    };

                    reportViewer.LocalReport.DataSources.Clear();
                    reportViewer.LocalReport.DataSources.Add(reportDataSource);
                    reportViewer.LocalReport.ReportPath = Path.Combine(reportsFolder, "rptOrder.rdlc");

                    string CompanyName = ConfigurationManager.AppSettings["CompanyName"]?.ToString() ?? "N/A";
                    string SellerAddress = ConfigurationManager.AppSettings["SellerAddress"]?.ToString() ?? "N/A";
                    string SellerTIN = ConfigurationManager.AppSettings["SellerTIN"]?.ToString() ?? "N/A";

                    Console.WriteLine($"CustomerName: {orderDto.CustomerName}"); // Để debug

                    // 5. Tạo tham số cho báo cáo
                    IList<ReportParameter> param = new List<ReportParameter>
                    {
                        new ReportParameter("Date", string.Format("Ngày {0} Tháng {1} Năm {2}",
                            orderDto.Date.Day,
                            orderDto.Date.Month,
                            orderDto.Date.Year)),

                        new ReportParameter("CompanyName", CompanyName),
                        new ReportParameter("SellerAddress", SellerAddress),
                        new ReportParameter("SellerTIN", SellerTIN),

                        new ReportParameter("BuyerName", orderDto.CustomerName), // Sử dụng orderDto.CustomerName
                        new ReportParameter("BuyerAddress", orderDto.CustomerAddress ?? string.Empty), // Thêm địa chỉ khách hàng nếu có trong DTO
                        new ReportParameter("BuyerTIN", orderDto.CustomerPhone ?? string.Empty), // Thêm SĐT khách hàng nếu có trong DTO (hoặc mã số thuế nếu có)

                        new ReportParameter("Total", detailsDto.Sum(d => d.Quantity * d.Price).ToString("N0")) // Định dạng số tiền
                    };

                    reportViewer.LocalReport.SetParameters(param);

                    // 6. Cài đặt chế độ xem và in
                    reportViewer.SetDisplayMode(DisplayMode.PrintLayout);
                    reportViewer.ZoomMode = ZoomMode.Percent;
                    reportViewer.ZoomPercent = 100;
                    reportViewer.RefreshReport();
                }
                else
                {
                    MessageBox.Show("Order not found or an error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading order for printing: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Có thể thêm log ex.StackTrace để debug chi tiết hơn
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private async void btnPrint_Click(object sender, EventArgs e) // Made async
        {
            OrderDTO orderDto = null;
            try
            {
                // Lấy lại thông tin orderDto để đảm bảo có ID mới nhất (nếu có)
                orderDto = await _clientService.GetOrderByIdAsync(id);

                // Check if have no data in ReportViewer
                if (reportViewer.LocalReport.DataSources.Count == 0 || orderDto == null)
                {
                    MessageBox.Show("No report data to save or order information is missing.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    saveFileDialog.FileName = "OrderReport_" + orderDto.ID.ToString() + ".pdf";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        //Save File PDF
                        File.WriteAllBytes(saveFileDialog.FileName, bytes);
                        MessageBox.Show("Order saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error when printing report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}