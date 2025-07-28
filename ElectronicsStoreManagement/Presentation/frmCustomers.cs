<<<<<<< HEAD
﻿using ClosedXML.Excel;
=======
using ClosedXML.Excel;
>>>>>>> 152c614542913596821ed33fe9be16cc33faa54d
using ElectronicsStore.Client;
using ElectronicsStore.DataTransferObject;
using Newtonsoft.Json; // Giữ lại nếu cần cho việc debug hoặc xử lý JSON riêng biệt, nhưng SendRequest đã làm phần lớn việc này
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

namespace ElectronicsStore.Presentation
{
    public partial class frmCustomers : Form
    {
        private readonly ClientService _clientService;
        bool signAdd = false;
        int currentCustomerId;

        private BindingSource binding = new BindingSource();

        public frmCustomers(ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();

            string helpURL = ConfigurationManager.AppSettings["HelpURL"]?.ToString();
            helpProvider1.HelpNamespace = helpURL + "customers.html";
        }

        private async void Customers_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            EnableControls(false);
            await LoadCustomers();
            SetupToolStrip();

            txtCustomerName.DataBindings.Clear();
            txtCustomerAddress.DataBindings.Clear();
            txtCustomerPhone.DataBindings.Clear();
            txtCustomerEmail.DataBindings.Clear();

            txtCustomerName.DataBindings.Add("Text", binding, "CustomerName", false, DataSourceUpdateMode.Never);
            txtCustomerAddress.DataBindings.Add("Text", binding, "CustomerAddress", false, DataSourceUpdateMode.Never);
            txtCustomerPhone.DataBindings.Add("Text", binding, "CustomerPhone", false, DataSourceUpdateMode.Never);
            txtCustomerEmail.DataBindings.Add("Text", binding, "CustomerEmail", false, DataSourceUpdateMode.Never);
            dataGridView.DataSource = binding;

            binding.CurrentChanged += (s, args) =>
            {
                // Optional: Any specific UI updates when current item changes
            };
        }

        private async Task LoadCustomers()
        {
            try
            {
                // ClientService.SendRequest đã tự xử lý ServerResponse và trả về Data trực tiếp
                List<CustomerDTO> customers = await _clientService.SendRequest<object, List<CustomerDTO>>("GetAllCustomer", null);
                if (customers != null)
                {
                    binding.DataSource = customers;
                    lblMessage.Text = string.Empty;
                }
                else
                {
                    // Nếu ClientService.SendRequest trả về null, có thể là không có dữ liệu hoặc lỗi không ném exception
                    MessageBox.Show("Failed to load customers. No data returned.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    binding.DataSource = new List<CustomerDTO>(); // Clear data if error
                }
            }
            catch (Exception ex)
            {
                // Mọi lỗi từ server (Success = false) hoặc lỗi mạng đều đã được ClientService gói gọn và ném ra
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                binding.DataSource = new List<CustomerDTO>(); // Clear data if error
            }
        }

        private void SetupToolStrip()
        {
            btnBegin.Click += (s, e) =>
            {
                if (binding.Count > 0)
                    binding.MoveFirst();
            };

            btnPrevious.Click += (s, e) =>
            {
                if (binding.Position > 0)
                    binding.MovePrevious();
            };

            btnNext.Click += (s, e) =>
            {
                if (binding.Position < binding.Count - 1)
                    binding.MoveNext();
            };

            btnEnd.Click += (s, e) =>
            {
                if (binding.Count > 0)
                    binding.MoveLast();
            };

            btnFind.Click += async (s, e) =>
            {
                string keyword = txtFind.Text.Trim();
                if (string.IsNullOrEmpty(keyword))
                {
                    await LoadCustomers();
                }
                else
                {
                    try
                    {
                        // ClientService.SendRequest đã tự xử lý ServerResponse và trả về Data trực tiếp
                        List<CustomerDTO> customers = await _clientService.SendRequest<string, List<CustomerDTO>>("SearchCustomers", keyword);
                        if (customers != null && customers.Any())
                        {
                            lblMessage.Text = string.Empty;
                            binding.DataSource = customers;
                        }
                        else
                        {
                            lblMessage.Text = "No matching customer found.";
                            binding.DataSource = new List<CustomerDTO>(); // Clear DataGridView
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error searching customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblMessage.Text = "Error during search.";
                        binding.DataSource = new List<CustomerDTO>(); // Clear data if error
                    }
                }
            };

            txtFind.TextChanged += async (s, e) => // Thay đổi để gọi lại LoadCustomers hoặc SearchCustomers
            {
                lblMessage.Text = string.Empty;
                // Có thể debounce hoặc yêu cầu click nút để tránh gửi request liên tục
                // Hiện tại giữ hành vi tìm kiếm tức thì như Category
                string keyword = txtFind.Text.Trim();
                if (string.IsNullOrEmpty(keyword))
                {
                    await LoadCustomers();
                }
                else
                {
                    try
                    {
                        List<CustomerDTO> customers = await _clientService.SendRequest<string, List<CustomerDTO>>("SearchCustomers", keyword);
                        if (customers != null && customers.Any())
                        {
                            lblMessage.Text = string.Empty;
                            binding.DataSource = customers;
                        }
                        else
                        {
                            lblMessage.Text = "No matching customer found.";
                            binding.DataSource = new List<CustomerDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi trong quá trình tìm kiếm khi TextChanged
                        Console.WriteLine($"Error during real-time search: {ex.Message}");
                        lblMessage.Text = "Error during search.";
                        binding.DataSource = new List<CustomerDTO>();
                    }
                }
            };
        }

        private void EnableControls(bool value)
        {
            btnSave.Enabled = value;
            btnCancel.Enabled = value;
            txtCustomerName.Enabled = value;
            txtCustomerAddress.Enabled = value;
            txtCustomerPhone.Enabled = value;
            txtCustomerEmail.Enabled = value;

            btnAdd.Enabled = !value;
            btnUpdate.Enabled = !value;
            btnDelete.Enabled = !value;
            btnFind.Enabled = !value;
            btnClear.Enabled = !value; // Đảm bảo nút Clear cũng được vô hiệu hóa/kích hoạt
            btnImport.Enabled = !value;
            btnExport.Enabled = !value;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            signAdd = true;
            EnableControls(true);
            txtCustomerName.Clear();
            txtCustomerAddress.Clear();
            txtCustomerPhone.Clear();
            txtCustomerEmail.Clear();
            binding.AddNew();
            txtCustomerName.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (binding.Current == null)
            {
                MessageBox.Show("Please select a customer to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            signAdd = false;
            EnableControls(true);
            CustomerDTO currentCustomer = binding.Current as CustomerDTO;
            if (currentCustomer != null)
            {
                currentCustomerId = currentCustomer.ID;
            }
            txtCustomerName.Focus();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = new CustomerDTO
                {
                    CustomerName = txtCustomerName.Text.Trim(),
                    CustomerAddress = txtCustomerAddress.Text.Trim(),
                    CustomerPhone = txtCustomerPhone.Text.Trim(),
                    CustomerEmail = txtCustomerEmail.Text.Trim()
                };

                if (string.IsNullOrEmpty(dto.CustomerName))
                {
                    MessageBox.Show("Customer Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // SendRequest sẽ ném ngoại lệ nếu có lỗi, nếu không thì coi như thành công
                // Vì ta không mong đợi dữ liệu trả về cụ thể sau khi thêm/sửa, dùng object làm TResponseData
                object result = null;

                if (signAdd)
                {
                    result = await _clientService.SendRequest<CustomerDTO, object>("AddCustomer", dto);
                }
                else
                {
                    dto.ID = currentCustomerId; // Gán lại ID cho DTO khi update
                    result = await _clientService.SendRequest<CustomerDTO, object>("UpdateCustomer", dto);
                }

                // Nếu không có ngoại lệ được ném, tức là thành công
                MessageBox.Show("Operation successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadCustomers(); // Tải lại dữ liệu sau khi lưu
                EnableControls(false);
            }
            catch (Exception ex)
            {
                // Mọi lỗi từ server (Success = false) hoặc lỗi mạng đều đã được ClientService gói gọn và ném ra
                MessageBox.Show($"Error saving customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (binding.Current == null)
            {
                MessageBox.Show("Please select a customer to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this customer?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    CustomerDTO selectedCustomer = binding.Current as CustomerDTO;
                    if (selectedCustomer != null)
                    {
                        int idToDelete = selectedCustomer.ID;
                        // SendRequest sẽ ném ngoại lệ nếu server báo lỗi hoặc có vấn đề mạng
                        await _clientService.SendRequest<int, object>("DeleteCustomer", idToDelete);

                        // Nếu không có ngoại lệ, tức là thành công
                        MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadCustomers(); // Tải lại dữ liệu sau khi xóa
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting customer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            EnableControls(false);
            await LoadCustomers(); // Tải lại dữ liệu gốc
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Import data from Excel file";
            openFileDialog.Filter = "Excel file|*.xls;*.xlsx";
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable table = new DataTable();
                    using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        IXLWorksheet worksheet = workbook.Worksheet(1);
                        bool firstRow = true;
                        string readRange = "1:1";
                        foreach (IXLRow row in worksheet.RowsUsed())
                        {
                            if (firstRow)
                            {
                                readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
                                foreach (IXLCell cell in row.Cells(readRange))
                                    table.Columns.Add(cell.Value.ToString());
                                firstRow = false;
                            }
                            else
                            {
                                table.Rows.Add();
                                int cellIndex = 0;
                                foreach (IXLCell cell in row.Cells(readRange))
                                {
                                    table.Rows[table.Rows.Count - 1][cellIndex] = cell.Value.ToString();
                                    cellIndex++;
                                }
                            }
                        }
                        if (firstRow)
                        {
                            MessageBox.Show("Excel file is empty or contains only headers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        int successCount = 0;
                        int errorCount = 0;
                        foreach (DataRow r in table.Rows)
                        {
                            try
                            {
                                var dto = new CustomerDTO
                                {
                                    CustomerName = r["CustomerName"].ToString(), // Đảm bảo tên cột khớp với Excel
                                    CustomerAddress = r["CustomerAddress"].ToString(),
                                    CustomerPhone = r["CustomerPhone"].ToString(),
                                    CustomerEmail = r["CustomerEmail"].ToString()
                                };

                                // SendRequest sẽ ném ngoại lệ nếu có lỗi, nếu không thì coi như thành công
                                await _clientService.SendRequest<CustomerDTO, object>("AddCustomer", dto);
                                successCount++;
                            }
                            catch (Exception ex)
                            {
                                errorCount++;
                                Console.WriteLine($"Error importing row '{r["CustomerName"]}': {ex.Message}");
                            }
                        }

                        MessageBox.Show($"{successCount} customers imported successfully. {errorCount} customers failed to import.", "Import Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadCustomers(); // Tải lại dữ liệu sau khi import
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during import: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export to Excel file";
            saveFileDialog.Filter = "Excel file|*.xls;*.xlsx";
            saveFileDialog.FileName = "Customers_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Lấy toàn bộ danh mục để xuất
                    List<CustomerDTO> customersToExport = await _clientService.SendRequest<object, List<CustomerDTO>>("GetAllCustomer", null);

                    if (customersToExport == null || !customersToExport.Any())
                    {
                        MessageBox.Show("No customers to export.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    // Tạo DataTable từ List<CustomerDTO>
                    DataTable table = new DataTable();
                    table.Columns.AddRange(new DataColumn[5]
                    {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("CustomerName", typeof(string)),
                        new DataColumn("CustomerAddress", typeof(string)),
                        new DataColumn("CustomerPhone", typeof(string)),
                        new DataColumn("CustomerEmail", typeof(string))
                    });

                    foreach (var p in customersToExport)
                        table.Rows.Add(p.ID, p.CustomerName, p.CustomerAddress, p.CustomerPhone, p.CustomerEmail);

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "Customers");
                        sheet.Columns().AdjustToContents();
                        wb.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Exported data to Excel file successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during export: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        // Thêm hàm btnClear_Click nếu nó tồn tại trên form của bạn
        private async void btnClear_Click(object sender, EventArgs e)
        {
            txtFind.Clear();
            await LoadCustomers(); // Tải lại toàn bộ danh sách
        }
    }
}
