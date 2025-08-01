﻿using System;
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
using System.IO;
using ElectronicsStore.Client; // Assuming ClientService is in this namespace

namespace ElectronicsStore.Presentation
{
    public partial class frmEmployees : Form
    {
        bool signAdd = false;
        int id;
        BindingSource binding = new BindingSource();
        private readonly ClientService _clientService;

        public frmEmployees(ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
            string helpURL = ConfigurationManager.AppSettings["HelpURL"]?.ToString() ?? string.Empty;
            helpProvider1.HelpNamespace = helpURL + "employees.html";
        }
        public frmEmployees()
        {
            _clientService = new ClientService("127.0.0.1", 301);
            InitializeComponent();
            string helpURL = ConfigurationManager.AppSettings["HelpURL"]?.ToString() ?? string.Empty;
            helpProvider1.HelpNamespace = helpURL + "employees.html";
        }

        private void EnableControls(bool value)
        {
            btnSave.Enabled = value;
            btnCancel.Enabled = value;
            txtEmployeeName.Enabled = value;
            txtEmployeeUsername.Enabled = value;
            txtEmployeePassword.Enabled = value;
            txtEmployeeAddress.Enabled = value;
            txtEmployeePhone.Enabled = value;
            cboRoles.Enabled = value;

            btnAdd.Enabled = !value;
            btnUpdate.Enabled = !value;
            btnDelete.Enabled = !value;
            btnFind.Enabled = !value;
            btnImport.Enabled = !value;
            btnExport.Enabled = !value;
            btnClear.Enabled = !value;
            txtFind.Enabled = !value;
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
                    await LoadEmployees();
                }
                else
                {
                    try
                    {
                        List<EmployeeDTO> employees = await _clientService.SendRequest<string, List<EmployeeDTO>>("SearchEmployees", keyword);
                        if (employees != null && employees.Any())
                        {
                            lblMessage.Text = string.Empty;
                            binding.DataSource = employees;
                        }
                        else
                        {
                            lblMessage.Text = "No matching employee found.";
                            binding.DataSource = new List<EmployeeDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi trong quá trình tìm kiếm khi TextChanged
                        Console.WriteLine($"Error during real-time search: {ex.Message}");
                        lblMessage.Text = "Error during search.";
                        binding.DataSource = new List<EmployeeDTO>();
                    }
                }
                

            };
        

            txtFind.TextChanged += async (s, e) =>
            {
                lblMessage.Text = string.Empty;
                // Có thể debounce hoặc yêu cầu click nút để tránh gửi request liên tục
                // Hiện tại giữ hành vi tìm kiếm tức thì như Category
                string keyword = txtFind.Text.Trim();
                if (string.IsNullOrEmpty(keyword))
                {
                    await LoadEmployees();
                }
                else
                {
                    try
                    {
                        List<EmployeeDTO> employees = await _clientService.SendRequest<string, List<EmployeeDTO>>("SearchEmployees", keyword);
                        if (employees != null && employees.Any())
                        {
                            lblMessage.Text = string.Empty;
                            binding.DataSource = employees;
                        }
                        else
                        {
                            lblMessage.Text = "No matching employee found.";
                            binding.DataSource = new List<EmployeeDTO>();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi trong quá trình tìm kiếm khi TextChanged
                        Console.WriteLine($"Error during real-time search: {ex.Message}");
                        lblMessage.Text = "Error during search.";
                        binding.DataSource = new List<EmployeeDTO>();
                    }
                }
            };
       
        }

        private async void Employees_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            EnableControls(false);

            cboRoles.Items.Clear();
            cboRoles.Items.Add(new { Text = "Admin", Value = true });
            cboRoles.Items.Add(new { Text = "Employee", Value = false });
            cboRoles.DisplayMember = "Text";
            cboRoles.ValueMember = "Value";

            await LoadEmployees(); // Call a dedicated method to load employees
            SetupToolStrip(); // Setup ToolStrip after data is loaded

            // Clear existing data bindings before re-adding
            txtEmployeeName.DataBindings.Clear();
            txtEmployeeUsername.DataBindings.Clear();
            txtEmployeeAddress.DataBindings.Clear();
            txtEmployeePhone.DataBindings.Clear();
            cboRoles.DataBindings.Clear();
            txtEmployeePassword.DataBindings.Clear(); // Ensure this is cleared, password is not bound directly

            txtEmployeeName.DataBindings.Add("Text", binding, "FullName", false, DataSourceUpdateMode.Never);
            txtEmployeeUsername.DataBindings.Add("Text", binding, "UserName", false, DataSourceUpdateMode.Never);
            txtEmployeeAddress.DataBindings.Add("Text", binding, "EmployeeAddress", false, DataSourceUpdateMode.Never);
            txtEmployeePhone.DataBindings.Add("Text", binding, "EmployeePhone", false, DataSourceUpdateMode.Never);

            // Custom binding for Role (boolean to selected index)
            Binding roleBinding = new Binding("SelectedIndex", binding, "Role", false, DataSourceUpdateMode.Never);
            roleBinding.Format += (s, args) =>
            {
                if (args.Value is bool roleBool)
                {
                    args.Value = roleBool ? 0 : 1; // 0 for Admin (true), 1 for Employee (false)
                }
            };
            cboRoles.DataBindings.Add(roleBinding);

            // Update password field explicitly if needed, but typically not bound directly
            binding.CurrentChanged += (s, args) =>
            {
                // Clear password field whenever selection changes to prevent display
                txtEmployeePassword.Text = "";
            };

            dataGridView.DataSource = binding;
        }

        private async Task LoadEmployees()
        {
            try
            {
                List<EmployeeDTO> employees = await _clientService.SendRequest<object, List<EmployeeDTO>>("GetAllEmployees", null);
                if (employees != null)
                {
                    binding.DataSource = employees;
                }
                else
                {
                    MessageBox.Show("Failed to load employees. No data returned.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    binding.DataSource = new List<EmployeeDTO>(); // Clear data if error
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                binding.DataSource = new List<EmployeeDTO>(); // Clear data if connection error
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            signAdd = true;
            EnableControls(true);
            txtEmployeeName.Focus();
            txtEmployeeName.Clear();
            txtEmployeeUsername.Clear();
            txtEmployeePassword.Clear();
            txtEmployeeAddress.Clear();
            txtEmployeePhone.Clear();
            cboRoles.SelectedIndex = -1; // Clear selection
            binding.AddNew(); // Add a new row to the BindingSource for input
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (binding.Current == null)
            {
                MessageBox.Show("Please select an employee to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            signAdd = false;
            EnableControls(true);
            EmployeeDTO currentEmployee = binding.Current as EmployeeDTO;
            if (currentEmployee != null)
            {
                id = currentEmployee.ID;
                txtEmployeePassword.Text = ""; // Clear password field for security
            }
            txtEmployeeName.Focus();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtEmployeeName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmployeeUsername.Text) ||
                    (signAdd && string.IsNullOrWhiteSpace(txtEmployeePassword.Text)) || // Password is required for Add
                    cboRoles.SelectedIndex == -1)
                {
                    MessageBox.Show("Please fill in all required fields (Name, Username, Password (for new employee), Role).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var employee = new EmployeeDTO
                {
                    FullName = txtEmployeeName.Text,
                    UserName = txtEmployeeUsername.Text,
                    EmployeeAddress = txtEmployeeAddress.Text,
                    EmployeePhone = txtEmployeePhone.Text,
                    Role = ((dynamic)cboRoles.SelectedItem).Value // Get the boolean value from selected item
                };

                // Only include password if it's for adding a new employee or if it's explicitly changed during update
                if (signAdd || !string.IsNullOrEmpty(txtEmployeePassword.Text))
                {
                    employee.Password = txtEmployeePassword.Text;
                }

                if (signAdd)
                {
                    await _clientService.SendRequest<EmployeeDTO, object>("AddEmployee", employee);
                }
                else
                {
                    employee.ID = id; // Ensure ID is set for update
                    await _clientService.SendRequest<EmployeeDTO, object>("UpdateEmployee", employee);
                }

                // If no exception is thrown, it means the operation was successful
                MessageBox.Show("Operation successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadEmployees(); // Reload data
                EnableControls(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving employee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (binding.Current == null)
            {
                MessageBox.Show("Please select an employee to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this employee?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    EmployeeDTO selectedEmployee = binding.Current as EmployeeDTO;
                    if (selectedEmployee != null)
                    {
                        int idToDelete = selectedEmployee.ID;
                        await _clientService.SendRequest<int, object>("DeleteEmployee", idToDelete);

                        // If no exception is thrown, it means the operation was successful
                        MessageBox.Show("Employee deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadEmployees(); // Reload data
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting employee: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            EnableControls(false);
            await LoadEmployees(); // Reload original data
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
                        string readRange = "1:1"; // Initialize with a default range

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

                        if (firstRow) // If only header row was present
                        {
                            MessageBox.Show("Excel file is empty or only contains headers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        int successCount = 0;
                        foreach (DataRow r in table.Rows)
                        {
                            try
                            {
                                // Handle potential nulls or incorrect types from Excel columns
                                var dto = new EmployeeDTO
                                {
                                    FullName = r["FullName"]?.ToString() ?? string.Empty,
                                    UserName = r["UserName"]?.ToString() ?? string.Empty,
                                    Password = r["Password"]?.ToString() ?? string.Empty,
                                    EmployeeAddress = r["EmployeeAddress"]?.ToString() ?? string.Empty,
                                    EmployeePhone = r["EmployeePhone"]?.ToString() ?? string.Empty,
                                    Role = bool.TryParse(r["Role"]?.ToString(), out bool role) ? role : false // Default to false if parsing fails
                                };

                                // Basic validation for essential fields from Excel
                                if (string.IsNullOrWhiteSpace(dto.FullName) || string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.Password))
                                {
                                    Console.WriteLine($"Skipping row due to missing required fields: FullName='{dto.FullName}', UserName='{dto.UserName}', Password='{dto.Password}'");
                                    continue;
                                }

                                await _clientService.SendRequest<EmployeeDTO, object>("AddEmployee", dto);
                                successCount++;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error processing row during import (Client-side): {ex.Message} - Data: {string.Join(", ", r.ItemArray)}");
                                // Log the error but continue processing other rows if possible
                                // Optionally, show a message for each failed row, or summarize at the end.
                            }
                        }

                        MessageBox.Show($"{successCount} row(s) imported successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadEmployees(); // Reload data
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error importing data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export to Excel file";
            saveFileDialog.Filter = "Excel file|*.xls;*.xlsx";
            saveFileDialog.FileName = "Employees_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    List<EmployeeDTO> employees = await _clientService.SendRequest<object, List<EmployeeDTO>>("GetAllEmployees", null);

                    if (employees == null)
                    {
                        MessageBox.Show("No employee data found to export.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    DataTable table = new DataTable();
                    table.Columns.AddRange(new DataColumn[]
                    {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("FullName", typeof(string)),
                        new DataColumn("EmployeePhone", typeof(string)),
                        new DataColumn("EmployeeAddress", typeof(string)),
                        new DataColumn("UserName", typeof(string)),
                        new DataColumn("Role", typeof(string)), // Export Role as string for readability
                    });

                    foreach (var em in employees)
                    {
                        table.Rows.Add(em.ID, em.FullName, em.EmployeePhone, em.EmployeeAddress, em.UserName, em.Role ? "Admin" : "Staff");
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "Employees");
                        sheet.Columns().AdjustToContents();
                        wb.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Exported data to Excel file successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private async void btnClear_Click(object sender, EventArgs e)
        {
            txtFind.Clear();
            lblMessage.Text = "";
            await LoadEmployees(); // Reload all employees
        }
    }
}