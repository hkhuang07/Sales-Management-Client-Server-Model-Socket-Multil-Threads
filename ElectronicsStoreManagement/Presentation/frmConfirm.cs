using ElectronicsStore.Client; // Assuming ClientService is in this namespace
using ElectronicsStore.DataTransferObject; // Make sure your DTOs are here
using Newtonsoft.Json; // For JSON serialization/deserialization
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; // Needed for ConfigurationManager
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronicsStore.Presentation
{
    public partial class frmConfirm : Form
    {
        private readonly ClientService _clientService; // Changed from ServerClientHandler to ClientService
        private bool _isAddingNewCustomer = true; // Renamed signAdd for clarity
        private int _selectedCustomerId = 0; // Renamed customerID for clarity

        public int OrderID { get; private set; }
        public int CustomerID { get; private set; }
        public int EmployeeID { get; private set; }
        public string Note { get; private set; }
        public bool PrintInvoice { get; private set; }
        public string CustomerName { get; private set; }
        public frmConfirm(ClientService clientService, int orderID = 0) // Inject ClientService
        {
            _clientService = clientService;
            InitializeComponent();
            OrderID = orderID;

        }

        public async Task LoadDataAsync()
        {
            try
            {
                // Tải danh sách nhân viên
                var employees = await _clientService.GetAllEmployeesAsync();
                if (employees != null)
                {
                    cboEmployee.DataSource = employees;
                    cboEmployee.DisplayMember = "FullName";
                    cboEmployee.ValueMember = "ID";
                }

                // Tải danh sách khách hàng
                var customers = await _clientService.GetAllCustomersAsync();
                if (customers != null)
                {
                    cboCustomer.DataSource = customers;
                    cboCustomer.DisplayMember = "CustomerName";
                    cboCustomer.ValueMember = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
        }

        public void EnableControls(bool value)
        {
            txtCustomerEmail.Enabled = value;
            txtCustomerPhone.Enabled = value;
            txtCustomerAddress.Enabled = value;
            txtNote.Enabled = value;
            cboEmployee.Enabled = value;
            cboCustomer.Enabled = !value; // Khi thêm mới thì không cho chọn ComboBox
            btnConfirm.Enabled = value;
            chkPrintInvoice.Enabled = value;

            btnAdd.Enabled = !value;
            btnUpdate.Enabled = !value;
        }

        private async void frmConfirm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync(); // Load data asynchronously
            EnableControls(false); // Initially disable controls until an action is chosen (Add/Update customer for the order)
            cboCustomer.SelectedIndex = -1;
            cboEmployee.SelectedIndex = -1; 
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            // Bước 1: Validate đầu vào
            if (string.IsNullOrEmpty(cboCustomer.Text.Trim()) || cboEmployee.SelectedValue == null)
            {
                MessageBox.Show("Please enter customer name and select an employee.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Bước 2: Xử lý thông tin khách hàng (Thêm mới hoặc Cập nhật)
            try
            {
                var customerDto = new CustomerDTO
                {
                    CustomerName = cboCustomer.Text.Trim(),
                    CustomerAddress = txtCustomerAddress.Text.Trim(),
                    CustomerPhone = txtCustomerPhone.Text.Trim(),
                    CustomerEmail = txtCustomerEmail.Text.Trim()
                };

                if (_isAddingNewCustomer)
                {
                    var addedCustomer = await _clientService.AddCustomerAsync(customerDto);
                    if (addedCustomer != null)
                    {
                        CustomerID = addedCustomer.ID;
                        CustomerName = addedCustomer.CustomerName;
                    }
                    else
                    {
                        MessageBox.Show("Failed to add new customer. Order confirmation aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    customerDto.ID = _selectedCustomerId;
                    bool updated = await _clientService.UpdateCustomerAsync(customerDto);
                    if (updated)
                    {
                        CustomerID = customerDto.ID;
                        CustomerName = customerDto.CustomerName;
                    }
                    else
                    {
                        MessageBox.Show("Failed to update customer information. Order confirmation aborted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Bước 3: Gán các giá trị cho các public property
                EmployeeID = (int)cboEmployee.SelectedValue;
                Note = txtNote.Text.Trim();
                PrintInvoice = chkPrintInvoice.Checked;

                // Bước 4: Đóng form với DialogResult.OK
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during confirmation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Giữ form mở để người dùng có thể sửa lỗi
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAddingNewCustomer = true;
            EnableControls(true);
            cboCustomer.SelectedIndex = -1;
            cboCustomer.Text = "";
            txtNote.Clear();
            txtCustomerEmail.Clear();
            txtCustomerPhone.Clear();
            txtCustomerAddress.Clear();
            cboCustomer.Focus();
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cboCustomer.SelectedValue == null)
            {
                MessageBox.Show("Please select a customer to update.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _isAddingNewCustomer = false;
            _selectedCustomerId = Convert.ToInt32(cboCustomer.SelectedValue);
            EnableControls(true);
            await LoadCustomerDetails(_selectedCustomerId);
        }


        private async void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCustomer.SelectedValue != null)
            {
                _selectedCustomerId = Convert.ToInt32(cboCustomer.SelectedValue);
                await LoadCustomerDetails(_selectedCustomerId);
            }
        }


        private async Task LoadCustomerDetails(int customerId)
        {
            try
            {
                var customer = await _clientService.GetCustomerByIdAsync(customerId);
                if (customer != null)
                {
                    txtCustomerAddress.Text = customer.CustomerAddress;
                    txtCustomerPhone.Text = customer.CustomerPhone;
                    txtCustomerEmail.Text = customer.CustomerEmail;
                    txtNote.Clear();
                    txtCustomerAddress.Focus();
                }
                else
                {
                    MessageBox.Show("Failed to load customer details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtCustomerAddress.Clear();
                    txtCustomerPhone.Clear();
                    txtCustomerEmail.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching customer details: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCustomerAddress.Clear();
                txtCustomerPhone.Clear();
                txtCustomerEmail.Clear();
            }
        }
    }
}