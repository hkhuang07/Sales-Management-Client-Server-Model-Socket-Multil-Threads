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
using ClosedXML.Excel;
using ElectronicsStore.Client;
using ElectronicsStore.DataTransferObject;

namespace ElectronicsStore.Presentation
{
    public partial class frmOrders : Form
    {
        private int id;

        BindingSource binding = new BindingSource();
        private readonly ClientService _clientService;
        // Khởi tạo ClientService ngay trong constructor của frmMain
        string serverIp = ConfigurationManager.AppSettings["ServerIp"] ?? "127.0.0.1"; // Default to localhost if not found
        int port = int.Parse(ConfigurationManager.AppSettings["ServerPort"] ?? "301"); // Default port

        public frmOrders(ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
            string helpURL = ConfigurationManager.AppSettings["HelpURL"]!.ToString();
            helpProvider1.HelpNamespace = helpURL + "orders.html";
        }
        public frmOrders()
        {
            _clientService = new ClientService("127.0.0.1", 301);
            InitializeComponent();
            string helpURL = ConfigurationManager.AppSettings["HelpURL"]!.ToString();
            helpProvider1.HelpNamespace = helpURL + "orders.html";
        }

        private void SetupToolStrip()
        {
            // Nút di chuyển đến đầu tiên
            btnBegin.Click += (s, e) =>
            {
                if (binding.Count > 0)
                    binding.MoveFirst();
                // DataGridView is already bound to binding, no need to re-assign
                // dataGridView.DataSource = binding; 
            };

            // Nút di chuyển đến dòng trước
            btnPrevious.Click += (s, e) =>
            {
                if (binding.Position > 0)
                    binding.MovePrevious();
                // DataGridView is already bound to binding, no need to re-assign
                // dataGridView.DataSource = binding;
            };

            // Nút di chuyển đến dòng tiếp theo
            btnNext.Click += (s, e) =>
            {
                if (binding.Position < binding.Count - 1)
                    binding.MoveNext();
                // DataGridView is already bound to binding, no need to re-assign
                // dataGridView.DataSource = binding;
            };

            // Nút di chuyển đến cuối cùng
            btnEnd.Click += (s, e) =>
            {
                if (binding.Count > 0)
                    binding.MoveLast();
                // DataGridView is already bound to binding, no need to re-assign
                // dataGridView.DataSource = binding;
            };

            // Nút tìm kiếm
            btnFind.Click += async (s, e) => // Made async to await client service calls
            {
                string keyword = txtFind.Text.Trim();
                try
                {
                    List<OrderDTO> filteredOrders;
                    if (string.IsNullOrEmpty(keyword))
                    {
                        // If search box is empty, load all data
                        filteredOrders = await _clientService.GetAllOrdersAsync();
                    }
                    else
                    {
                        // Call server to search orders by keyword
                        filteredOrders = await _clientService.SearchOrdersAsync(keyword);
                    }

                    binding.DataSource = filteredOrders;
                    // DataGridView is already bound to binding, no need to re-assign
                    // dataGridView.DataSource = binding; 
                    if (filteredOrders == null || filteredOrders.Count == 0)
                    {
                        lblMessage.Text = "No matching order found.";
                    }
                    else
                    {
                        lblMessage.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error connecting to server or searching orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            txtFind.TextChanged += (s, e) =>
            {
                lblMessage.Text = ""; // Clear the message label when text changes
                // No longer automatically trigger btnFind.PerformClick() on TextChanged
                // because it causes a new async call on every keystroke, which can be inefficient.
                // The user can manually click the Find button or press Enter (if implemented).
            };
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow != null)
            {
                id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
                using (frmPrintOrder printOrder = new frmPrintOrder(id, _clientService))
                {
                    printOrder.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Please select an order to print.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Trong frmOrders
        private async Task LoadOrdersDataAsync()
        {
            try
            {
                List<OrderDTO> orderList = await _clientService.GetAllOrdersAsync();
                binding.DataSource = orderList;
                // Không cần gán lại dataGridView.DataSource = binding; nếu nó đã được gán một lần trong Load
                // dataGridView.DataSource = binding; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void frmOrders_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            if (!dataGridView.Columns.Contains("ViewDetails"))
            {
                DataGridViewLinkColumn viewDetailsColumn = new DataGridViewLinkColumn();
                viewDetailsColumn.Name = "ViewDetails";
                viewDetailsColumn.HeaderText = "Chi tiết";
                viewDetailsColumn.Text = "View Details";
                viewDetailsColumn.UseColumnTextForLinkValue = true;
                viewDetailsColumn.LinkColor = Color.Blue;
                viewDetailsColumn.TrackVisitedState = false;
                dataGridView.Columns.Add(viewDetailsColumn);
            }

            binding.DataSource = new List<OrderDTO>(); // Khởi tạo rỗng để tránh lỗi null reference ban đầu
            dataGridView.DataSource = binding; // Gán DataGridView với BindingSource một lần duy nhất
            SetupToolStrip();

            await LoadOrdersDataAsync(); // Lần đầu tiên tải dữ liệu khi form load
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            using (frmOrderDetails orderDetails = new frmOrderDetails(_clientService, 0))
            {
                if (orderDetails.ShowDialog() == DialogResult.OK)
                {
                    // Await the data reload to ensure it completes before continuing
                    await LoadOrdersDataAsync();
                }
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow != null)
            {
                id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value);
                using (frmOrderDetails orderDetails = new frmOrderDetails(_clientService, id))
                {
                    if (orderDetails.ShowDialog() == DialogResult.OK)
                    {
                        // Await the data reload
                        await LoadOrdersDataAsync();

                    }
                    await LoadOrdersDataAsync();

                }
            }
            else
            {
                MessageBox.Show("Please select an order to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void bntDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Please select an order to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int idToDelete = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value);
                    bool success = await _clientService.DeleteOrderAsync(idToDelete);

                    if (success)
                    {
                        MessageBox.Show("Order deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadOrdersDataAsync(); // Await the data reload
                    }
                    else
                    {
                        MessageBox.Show("Watting for server updating order.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    await LoadOrdersDataAsync(); // Await the data reload

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /*
         private async void frmOrders_Load(object sender, EventArgs e) // Made async
        {
            dataGridView.AutoGenerateColumns = false;
            try
            {
                if (!dataGridView.Columns.Contains("ViewDetails"))
                {
                    DataGridViewLinkColumn viewDetailsColumn = new DataGridViewLinkColumn();
                    viewDetailsColumn.Name = "ViewDetails";
                    viewDetailsColumn.HeaderText = "Chi tiết"; // Tiêu đề hiển thị cho cột
                    viewDetailsColumn.Text = "View Details"; // Văn bản liên kết hiển thị trong mỗi ô
                    viewDetailsColumn.UseColumnTextForLinkValue = true; // Rất quan trọng: Bắt buộc dùng thuộc tính Text để hiển thị
                    viewDetailsColumn.LinkColor = Color.Blue; // Tùy chỉnh màu sắc liên kết
                    viewDetailsColumn.TrackVisitedState = false; // Tùy chọn: không thay đổi màu sau khi click
                    dataGridView.Columns.Add(viewDetailsColumn);
                }
                List<OrderDTO> orderList = await _clientService.GetAllOrdersAsync();
                binding.DataSource = orderList;
                SetupToolStrip(); // Setup tool strip after binding source is initialized
                dataGridView.DataSource = binding; // Bind DataGridView to binding source
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (frmOrderDetails orderDetails = new frmOrderDetails(_clientService, 0)) // Thêm mới, nên orderID = 0
            {
                if (orderDetails.ShowDialog() == DialogResult.OK)
                {
                    // This assumes frmOrderDetails handles its own client service calls for add/update.
                    // After it closes successfully, reload the orders list.
                    frmOrders_Load(sender, e); // Reload lại danh sách đơn hàng
                }
            }
        }
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow != null)
            {
                id = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value);
                using (frmOrderDetails orderDetails = new frmOrderDetails(_clientService, id))
                {
                    if (orderDetails.ShowDialog() == DialogResult.OK)
                    {
                        // This assumes frmOrderDetails handles its own client service calls for add/update.
                        // After it closes successfully, reload the orders list.
                        frmOrders_Load(sender, e); // Reload lại danh sách đơn hàng sau khi cập nhật
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an order to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void bntDelete_Click(object sender, EventArgs e) // Made async
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Please select an order to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this order?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int idToDelete = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value);
                    bool success = await _clientService.DeleteOrderAsync(idToDelete); // Use client service

                    if (success)
                    {
                        MessageBox.Show("Order deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmOrders_Load(sender, e); // Reload data after deletion
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting order: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }*/

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export to Excel file";
            saveFileDialog.Filter = "Excel file|*.xls;*.xlsx";
            saveFileDialog.FileName = "Orders_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable table = new DataTable();
                    table.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("Date", typeof(DateTime)),
                        new DataColumn("EmployeeName", typeof(string)),
                        new DataColumn("CustomerName", typeof(string)),
                        new DataColumn("TotalPrice", typeof(double)),
                        new DataColumn("Note", typeof(string)),
                        new DataColumn("ViewDetails", typeof(string))
                    });

                    // Use the data currently in the binding source to export
                    List<OrderDTO> ordersToExport = binding.DataSource as List<OrderDTO>;

                    if (ordersToExport != null)
                    {
                        foreach (var ord in ordersToExport)
                        {
                            table.Rows.Add(ord.ID, ord.Date, ord.EmployeeName, ord.CustomerName, ord.TotalPrice, ord.Note, "ViewDetails"); // Add a placeholder for ViewDetails
                        }
                    }
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "Orders");
                        sheet.Columns().AdjustToContents();
                        wb.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Exported data to Excel file successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView.Columns["ViewDetails"].Index)
            {
                int orderId = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells["ID"].Value);
                using (frmOrderDetails orderDetails = new frmOrderDetails(_clientService, orderId))
                {
                    orderDetails.ShowDialog();
                }
            }
        }

        private async void btnClear_Click(object sender, EventArgs e) // Made async
        {
            txtFind.Clear();
            try
            {
                binding.DataSource = await _clientService.GetAllOrdersAsync(); // Use client service
                // DataGridView is already bound to binding, no need to re-assign
                // dataGridView.DataSource = binding; 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing search and reloading orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
    }
}