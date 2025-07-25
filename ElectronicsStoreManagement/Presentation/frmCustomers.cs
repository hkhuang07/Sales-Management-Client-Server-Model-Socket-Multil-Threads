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
using ElectronicsStore.DataTransferObject;
using Newtonsoft.Json;
using ElectronicsStore.Client; // Assuming ClientService is in this namespace

namespace ElectronicsStore.Presentation
{
    public partial class frmCustomers : Form
    {
        private readonly ClientService _clientService;
        bool signAdd = false;
        int currentCustomerId;

        private BindingSource binding = new BindingSource();

        public frmCustomers(ClientService clientService) // Use ClientService here
        {
            _clientService = clientService; // Assign the injected ClientService
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
                // Use _clientService.SendRequest with explicit generic types
                ServerResponse<List<CustomerDTO>> response = await _clientService.SendRequest<object, ServerResponse<List<CustomerDTO>>>("GetAllCustomer", null);
                if (response.Success && response.Data != null)
                {
                    binding.DataSource = response.Data;
                    lblMessage.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show($"Error loading customers: {response.Message ?? "Unknown error"}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    binding.DataSource = new List<CustomerDTO>(); // Clear data if error
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred while loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        // Use _clientService.SendRequest for search
                        ServerResponse<List<CustomerDTO>> response = await _clientService.SendRequest<string, ServerResponse<List<CustomerDTO>>>("SearchCustomers", keyword);
                        if (response.Success && response.Data != null)
                        {
                            if (response.Data.Count == 0)
                            {
                                lblMessage.Text = "No matching customer found.";
                            }
                            else
                            {
                                lblMessage.Text = string.Empty;
                            }
                            binding.DataSource = response.Data;
                        }
                        else
                        {
                            MessageBox.Show($"Error searching customers: {response.Message ?? "Unknown error"}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            binding.DataSource = new List<CustomerDTO>(); // Clear data if error
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An unexpected error occurred during search: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        binding.DataSource = new List<CustomerDTO>(); // Clear data if error
                    }
                }
            };

            txtFind.TextChanged += (s, e) =>
            {
                lblMessage.Text = string.Empty;
                // Consider debouncing this or requiring an explicit button click for network operations
                // For now, keeping the existing behavior of immediate search
                btnFind.PerformClick();
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

                // Use ServerResponse<object> as the return type if the server only confirms success/failure
                ServerResponse<object> response;

                if (signAdd)
                {
                    response = await _clientService.SendRequest<CustomerDTO, ServerResponse<object>>("AddCustomer", dto);
                }
                else
                {
                    dto.ID = currentCustomerId;
                    response = await _clientService.SendRequest<CustomerDTO, ServerResponse<object>>("UpdateCustomer", dto);
                }

                if (response.Success)
                {
                    MessageBox.Show("Operation successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadCustomers();
                    EnableControls(false);
                }
                else
                {
                    MessageBox.Show(response.Message ?? "Operation failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
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
                        // Use ServerResponse<object> as the return type for delete
                        ServerResponse<object> response = await _clientService.SendRequest<int, ServerResponse<object>>("DeleteCustomer", idToDelete);

                        if (response.Success)
                        {
                            MessageBox.Show("Customer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadCustomers();
                        }
                        else
                        {
                            MessageBox.Show(response.Message ?? "Deletion failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
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
            await LoadCustomers();
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
                            MessageBox.Show("Excel file is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                                    CustomerName = r["CustomerName"].ToString(),
                                    CustomerAddress = r["CustomerAddress"].ToString(),
                                    CustomerPhone = r["CustomerPhone"].ToString(),
                                    CustomerEmail = r["CustomerEmail"].ToString()
                                };

                                // Send each CustomerDTO to the Server for adding
                                ServerResponse<object> response = await _clientService.SendRequest<CustomerDTO, ServerResponse<object>>("AddCustomer", dto);
                                if (response.Success)
                                {
                                    successCount++;
                                }
                                else
                                {
                                    errorCount++;
                                    Console.WriteLine($"Error importing row '{dto.CustomerName}': {response.Message}");
                                }
                            }
                            catch (Exception ex)
                            {
                                errorCount++;
                                Console.WriteLine($"Error processing row for import: {ex.Message}");
                            }
                        }

                        MessageBox.Show($"{successCount} customers imported successfully. {errorCount} customers failed to import.", "Import Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadCustomers();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    // Get all customers from Server
                    ServerResponse<List<CustomerDTO>> response = await _clientService.SendRequest<object, ServerResponse<List<CustomerDTO>>>("GetAllCustomer", null);
                    if (!response.Success || response.Data == null)
                    {
                        MessageBox.Show(response.Message ?? "Failed to retrieve customers for export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var customersToExport = response.Data;

                    DataTable table = new DataTable();
                    table.Columns.AddRange(new DataColumn[5]
                    {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("CustomerName", typeof(string)),
                        new DataColumn("CustomerAddress", typeof(string)),
                        new DataColumn("CustomerPhone", typeof(string)),
                        new DataColumn("CustomerEmail", typeof(string))
                    });

                    foreach (var c in customersToExport)
                        table.Rows.Add(c.ID, c.CustomerName, c.CustomerAddress, c.CustomerPhone, c.CustomerEmail);

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
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }
    }
}