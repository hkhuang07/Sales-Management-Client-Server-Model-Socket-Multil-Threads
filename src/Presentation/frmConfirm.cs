using DocumentFormat.OpenXml.Wordprocessing;
using ElectronicsStore.Client;
using ElectronicsStore.DataTransferObject;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
        private readonly ClientService _clientService;
        private bool _isAddingNewCustomer = true;
        private int _selectedCustomerId = 0;

        public int OrderID { get; private set; }
        public int CustomerID { get; private set; }
        public int EmployeeID { get; private set; }
        public string Note { get; private set; }
        public bool PrintInvoice { get; private set; }
        public string CustomerName { get; private set; }

        // Sử dụng một constructor duy nhất, truyền OrderID và EmployeeID từ Main Form.
        public frmConfirm(ClientService clientService, int orderID, int employeeID)
        {
            _clientService = clientService;
            InitializeComponent();
            OrderID = orderID;
            EmployeeID = employeeID;
        }

        // Hàm này sẽ tải dữ liệu từ server và hiển thị lên các ComboBox
        public async Task LoadDataAsync()
        {
            try
            {
                // Chỉ tải thông tin nhân viên của người dùng hiện tại
                if (EmployeeID > 0)
                {
                    var employee = await _clientService.GetEmployeeByIdAsync(EmployeeID);
                    if (employee != null)
                    {
                        var employeeList = new List<EmployeeDTO> { employee };
                        cboEmployee.DataSource = employeeList;
                        cboEmployee.DisplayMember = "FullName";
                        cboEmployee.ValueMember = "ID";
                        cboEmployee.SelectedValue = EmployeeID;
                    }
                }
                else
                {
                    // Nếu EmployeeID không hợp lệ, tải toàn bộ danh sách nhân viên
                    var employees = await _clientService.GetAllEmployeesAsync();
                    if (employees != null)
                    {
                        cboEmployee.DataSource = employees;
                        cboEmployee.DisplayMember = "FullName";
                        cboEmployee.ValueMember = "ID";
                    }
                }
                cboEmployee.Enabled = false; // Luôn tắt ComboBox nhân viên

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

        private async Task LoadCustomerDetails(int customerId)
        {
            try
            {
                var customer = await _clientService.GetCustomerByIdAsync(customerId);
                if (customer != null)
                {
                    // Binding lại các trường textbox
                    txtCustomerAddress.Text = customer.CustomerAddress;
                    txtCustomerPhone.Text = customer.CustomerPhone;
                    txtCustomerEmail.Text = customer.CustomerEmail;
                }
                else
                {
                    MessageBox.Show("Failed to load customer details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearCustomerFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching customer details: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearCustomerFields();
            }
        }

        // Cập nhật hàm EnableControls để phù hợp với logic mới
        public void EnableControls(bool value)
        {
            txtCustomerEmail.Enabled = value;
            txtCustomerPhone.Enabled = value;
            txtCustomerAddress.Enabled = value;
            txtNote.Enabled = value;
            // cboEmployee.Enabled = false; // ComboBox nhân viên luôn bị tắt
            btnConfirm.Enabled = value;
            chkPrintInvoice.Enabled = value;

            btnAdd.Enabled = !value;
            btnUpdate.Enabled = !value;
        }

        private void ClearFields()
        {
            chkPrintInvoice.Checked = false;
            txtNote.Clear();
            ClearCustomerFields();
        }

        private void ClearCustomerFields()
        {
            txtCustomerAddress.Clear();
            txtCustomerPhone.Clear();
            txtCustomerEmail.Clear();
        }

        private async void frmConfirm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync();

            // Phân biệt chế độ ADD và UPDATE
            if (OrderID <= 0) // Đây là đơn hàng mới (ADD)
            {
                _isAddingNewCustomer = true;
                EnableControls(true);
                cboCustomer.SelectedIndex = -1; // ComboBox khách hàng rỗng
                cboCustomer.Text = "";
                cboCustomer.Enabled = true;
            }
            else // Đây là đơn hàng cũ (UPDATE)
            {
                _isAddingNewCustomer = false;
                EnableControls(false); // Ban đầu vô hiệu hóa các trường
                var order = await _clientService.GetOrderByIdAsync(OrderID);
                if (order != null)
                {
                    // Load thông tin khách hàng và nhân viên
                    cboCustomer.SelectedValue = order.CustomerID;
                    txtNote.Text = order.Note;
                    await LoadCustomerDetails(order.CustomerID);
                }
            }
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboCustomer.Text.Trim()) || cboEmployee.SelectedValue == null)
            {
                MessageBox.Show("Please enter customer name and select an employee.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var customerDto = new CustomerDTO
                {
                    CustomerName = cboCustomer.Text.Trim(),
                    CustomerAddress = txtCustomerAddress.Text.Trim(),
                    CustomerPhone = txtCustomerPhone.Text.Trim(),
                    CustomerEmail = txtCustomerEmail.Text.Trim()
                };

                // Logic xử lý khách hàng đã được tách ra rõ ràng
                if (_isAddingNewCustomer)
                {
                    var addedCustomer = await _clientService.AddCustomerAsync(customerDto);
                    if (addedCustomer != null && addedCustomer.ID > 0)
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
                else // Đơn hàng cũ, cập nhật thông tin khách hàng nếu cần
                {
                    if (cboCustomer.SelectedValue != null)
                    {
                        customerDto.ID = Convert.ToInt32(cboCustomer.SelectedValue);
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
                    else
                    {
                        MessageBox.Show("Please select a customer to update.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Gán các giá trị và đóng form
                EmployeeID = (int)cboEmployee.SelectedValue;
                Note = txtNote.Text.Trim();
                PrintInvoice = chkPrintInvoice.Checked;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during confirmation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAddingNewCustomer = true;
            cboCustomer.SelectedIndex = -1;
            cboCustomer.Text = "";
            cboCustomer.Enabled = true;
            ClearCustomerFields();
            EnableControls(true);
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
            cboCustomer.Enabled = false;
            EnableControls(true);
            await LoadCustomerDetails(Convert.ToInt32(cboCustomer.SelectedValue));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmConfirm_Load(sender, e);
        }

        private async void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isAddingNewCustomer && cboCustomer.SelectedValue != null)
            {
                await LoadCustomerDetails(Convert.ToInt32(cboCustomer.SelectedValue));
            }
            else
            {
                ClearCustomerFields();
            }
        }
    }
}