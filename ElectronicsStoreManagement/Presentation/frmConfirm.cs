using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration; // Needed for ConfigurationManager
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectronicsStore.DataTransferObject; // Make sure your DTOs are here
using Newtonsoft.Json; // For JSON serialization/deserialization
using ElectronicsStore.Client; // Assuming ClientService is in this namespace

namespace ElectronicsStore.Presentation
{
    public partial class frmConfirm : Form
    {
        private readonly ClientService _clientService; // Changed from ServerClientHandler to ClientService
        private bool _isAddingNewCustomer = true; // Renamed signAdd for clarity
        private int _selectedCustomerId = 0; // Renamed customerID for clarity

        public int OrderID { get; private set; }
        public int CustomerID { get; private set; } // This might be set after a new customer is added or selected

        public frmConfirm(ClientService clientService, int orderID = 0) // Inject ClientService
        {
            _clientService = clientService;

            InitializeComponent();
            OrderID = orderID;

        }

        /// <summary>
        /// Loads employee and customer data from the server into their respective ComboBoxes.
        /// </summary>
        public async Task LoadDataAsync() // Changed to async Task
        {
            try
            {
                // Load Employees
                // Use _clientService.SendRequest with explicit generic types for request and response
                ServerResponse<List<EmployeeDTO>> employeeResponse = await _clientService.SendRequest<object, ServerResponse<List<EmployeeDTO>>>("GetAllEmployees", null);
                if (employeeResponse.Success && employeeResponse.Data != null)
                {
                    cboEmployee.DataSource = employeeResponse.Data;
                    cboEmployee.DisplayMember = "FullName"; // Assuming EmployeeDTO has FullName
                    cboEmployee.ValueMember = "ID"; // Assuming EmployeeDTO has ID
                }
                else
                {
                    MessageBox.Show($"Error loading employees: {employeeResponse.Message ?? "Unknown error"}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Load Customers
                ServerResponse<List<CustomerDTO>> customerResponse = await _clientService.SendRequest<object, ServerResponse<List<CustomerDTO>>>("GetAllCustomer", null);
                if (customerResponse.Success && customerResponse.Data != null)
                {
                    cboCustomer.DataSource = customerResponse.Data;
                    cboCustomer.DisplayMember = "CustomerName"; // Assuming CustomerDTO has CustomerName
                    cboCustomer.ValueMember = "ID"; // Assuming CustomerDTO has ID
                }
                else
                {
                    MessageBox.Show($"Error loading customers: {customerResponse.Message ?? "Unknown error"}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading data: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void EnableControls(bool value)
        {
            txtCustomerEmail.Enabled = value;
            txtCustomerPhone.Enabled = value;
            txtCustomerAddress.Enabled = value;
            txtNote.Enabled = value;
            cboEmployee.Enabled = value;
            btnConfirm.Enabled = value;
            chkPrintInvoice.Enabled = value;

            btnAdd.Enabled = !value;
            btnUpdate.Enabled = !value;
        }

        private async void frmConfirm_Load(object sender, EventArgs e)
        {
            await LoadDataAsync(); // Load data asynchronously
            EnableControls(false); // Initially disable controls until an action is chosen (Add/Update customer for the order)

            // If an OrderID is passed, you might want to load existing order details
            // For now, focusing on customer and employee selection
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            // First, handle customer information (add new or update existing)
            CustomerDTO customerDto = new CustomerDTO
            {
                CustomerName = cboCustomer.Text.Trim(), // If cboCustomer is editable or you use txtCustomerName
                CustomerAddress = txtCustomerAddress.Text.Trim(),
                CustomerPhone = txtCustomerPhone.Text.Trim(),
                CustomerEmail = txtCustomerEmail.Text.Trim()
            };

            // Basic validation for customer fields
            if (string.IsNullOrEmpty(customerDto.CustomerName))
            {
                MessageBox.Show("Customer Name cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Add more validation as needed (phone format, email format, etc.)

            try
            {
                ServerResponse<CustomerDTO> customerResponse; // Expecting CustomerDTO back, especially for Add
                if (_isAddingNewCustomer)
                {
                    customerResponse = await _clientService.SendRequest<CustomerDTO, ServerResponse<CustomerDTO>>("AddCustomer", customerDto);
                }
                else
                {
                    customerDto.ID = _selectedCustomerId; // Ensure ID is set for update
                    customerResponse = await _clientService.SendRequest<CustomerDTO, ServerResponse<CustomerDTO>>("UpdateCustomer", customerDto);
                }

                if (!customerResponse.Success)
                {
                    MessageBox.Show($"Customer operation failed: {customerResponse.Message ?? "Unknown error"}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // If adding a new customer, the server should return the new CustomerID
                // Assuming the server returns the updated/added CustomerDTO with ID in its Data field
                if (customerResponse.Data != null)
                {
                    CustomerID = customerResponse.Data.ID;
                }
                else
                {
                    // Fallback if Data is null, for updates, use the selected ID
                    CustomerID = _selectedCustomerId;
                }

                // Now, proceed with confirming the order
                // This is where you'd typically send an "ConfirmOrder" request with OrderID, CustomerID, EmployeeID, and Notes
                var confirmOrderRequestData = new
                {
                    OrderId = OrderID,
                    CustomerId = CustomerID,
                    EmployeeId = (int)cboEmployee.SelectedValue!,
                    Note = txtNote.Text.Trim(),
                    PrintInvoice = chkPrintInvoice.Checked
                };

                // ServerResponse<object> as the return type if the server only confirms success/failure for order confirmation
                ServerResponse<object> orderResponse = await _clientService.SendRequest<object, ServerResponse<object>>("ConfirmOrder", confirmOrderRequestData);

                if (orderResponse.Success)
                {
                    MessageBox.Show("Order confirmed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show($"Order confirmation failed: {orderResponse.Message ?? "Unknown error"}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during confirmation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _isAddingNewCustomer = true;
            EnableControls(true);

            // Clear customer input fields when adding a new customer
            cboCustomer.SelectedIndex = -1; // Deselect any existing customer
            cboCustomer.Text = ""; // Clear text if it's editable
            txtNote.Clear();
            txtCustomerEmail.Clear();
            txtCustomerPhone.Clear();
            txtCustomerAddress.Clear();
            cboCustomer.Focus(); // Focus on customer name for new entry
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

            // Load selected customer's details from server
            await LoadCustomerDetails(_selectedCustomerId); // Call the dedicated method
        }

        private async void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // When a customer is selected from the ComboBox, load their details
            // This is useful if the user selects an existing customer instead of adding a new one.
            if (cboCustomer.SelectedValue != null && !_isAddingNewCustomer) // Only auto-fill if not in "Add New Customer" mode
            {
                _selectedCustomerId = Convert.ToInt32(cboCustomer.SelectedValue);
                // The original code here was causing the "cannot await void" error.
                // It has been changed to await the LoadCustomerDetails method properly.
                await LoadCustomerDetails(_selectedCustomerId);
            }
        }

        // Changed from async void to async Task to allow awaiting
        private async Task LoadCustomerDetails(int customerId)
        {
            try
            {
                // Use _clientService.SendRequest for GetCustomerById
                ServerResponse<CustomerDTO> response = await _clientService.SendRequest<int, ServerResponse<CustomerDTO>>("GetCustomerById", customerId);
                if (response.Success && response.Data != null)
                {
                    var customer = response.Data;
                    txtCustomerAddress.Text = customer.CustomerAddress;
                    txtCustomerPhone.Text = customer.CustomerPhone;
                    txtCustomerEmail.Text = customer.CustomerEmail;
                    // cboCustomer.Text is already set by the selection
                    txtNote.Clear(); // Clear note for new confirmation process
                    txtCustomerAddress.Focus();
                }
                else
                {
                    MessageBox.Show($"Failed to load customer details: {response.Message ?? "Unknown error"}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Clear fields if loading fails
                    txtCustomerAddress.Clear();
                    txtCustomerPhone.Clear();
                    txtCustomerEmail.Clear();
                    EnableControls(false); // Optionally disable controls if details can't be loaded for update
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching customer details: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Clear fields if loading fails
                txtCustomerAddress.Clear();
                txtCustomerPhone.Clear();
                txtCustomerEmail.Clear();
                EnableControls(false); // Optionally disable controls if details can't be loaded for update
            }
        }
    }
}