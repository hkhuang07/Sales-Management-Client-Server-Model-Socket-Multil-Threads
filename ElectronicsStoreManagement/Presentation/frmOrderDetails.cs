using ElectronicsStore.Client; // Use ClientService directly
using ElectronicsStore.DataTransferObject;
using Newtonsoft.Json; // Still needed for general JSON operations if you perform them outside ClientService
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
    public partial class frmOrderDetails : Form
    {
        private readonly ClientService _clientService; // Changed from ServerClientHandler
        private BindingList<OrderDetailsDTO> orderDetails;

        public int OrderID { get; set; }
        public int EmployeeID { get; set; }
        public int CustomerID { get; set; }

        public frmOrderDetails(ClientService clientSercive, int orderID = 0)
        {

            // Initialize ClientService
            //_clientService = new ClientService(ConfigurationManager.AppSettings["ServerIp"], int.Parse(ConfigurationManager.AppSettings["ServerPort"]));
            _clientService = clientSercive;
            OrderID = orderID;
            InitializeComponent();

            string helpURL = ConfigurationManager.AppSettings["HelpURL"]?.ToString();
            helpProvider1.HelpNamespace = helpURL + "orderdetails.html"; // Fixed typo in "orderdetails"
        }

        private async Task LoadData()
        {
            // Load Employees
            try
            {
                var employees = await _clientService.GetAllEmployeesAsync();
                cboEmployee.DataSource = employees;
                cboEmployee.DisplayMember = "FullName";
                cboEmployee.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading employees: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Load Customers
            try
            {
                var customers = await _clientService.GetAllCustomersAsync();
                cboCustomer.DataSource = customers;
                cboCustomer.DisplayMember = "CustomerName";
                cboCustomer.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading customers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Load Products
            try
            {
                var products = await _clientService.GetAllProductsAsync();
                cboProduct.DataSource = products;
                cboProduct.DisplayMember = "ProductName";
                cboProduct.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (EmployeeID != 0 && cboEmployee.Items.Cast<EmployeeDTO>().Any(e => e.ID == EmployeeID))
                cboEmployee.SelectedValue = EmployeeID;
            else
                cboEmployee.SelectedIndex = -1; // Ensure no selection if EmployeeID is not found

            if (CustomerID != 0 && cboCustomer.Items.Cast<CustomerDTO>().Any(c => c.ID == CustomerID))
                cboCustomer.SelectedValue = CustomerID;
            else
                cboCustomer.SelectedIndex = -1; // Ensure no selection if CustomerID is not found
        }

        public void EnableControls()
        {
            if (OrderID == 0 && orderDetails.Count == 0) // Add mode and no details added yet
            {
                cboCustomer.SelectedIndex = -1;
                cboEmployee.SelectedIndex = -1;
                cboProduct.SelectedIndex = -1;
                numQuantity.Value = 1;
                numPrice.Value = 0;
            }

            btnSave.Enabled = orderDetails.Count > 0; // Check orderDetails collection, not DataGridView rows
            btnDelete.Enabled = dataGridView.SelectedRows.Count > 0; // Enable delete only if a row is selected
        }

        private async void frmOrderDetails_Load(object sender, EventArgs e)
        {
                await LoadData(); // Await LoadData to ensure comboboxes are populated
            dataGridView.AutoGenerateColumns = false;

            if (OrderID != 0)
            {
                // Get Order
                try
                {
                    var order = await _clientService.GetOrderByIdAsync(OrderID);
                    if (order != null)
                    {
                        cboEmployee.SelectedValue = order.EmployeeID;
                        cboCustomer.SelectedValue = order.CustomerID;
                        txtNote.Text = order.Note;
                        //dtpOrderDate.Value = order.Date; // Assuming you have a DateTimePicker for order date

                        // Get Order Details
                        var details = await _clientService.GetOrderDetailsByOrderIdAsync(OrderID);

                        // Populate ProductName for each detail (if not already handled by server)
                        foreach (var item in details)
                        {
                            try
                            {
                                var product = await _clientService.GetProductByIdAsync(item.ProductID);
                                item.ProductName = product?.ProductName ?? "Unknown Product";
                            }
                            catch (Exception prodEx)
                            {
                                item.ProductName = "Unknown Product";
                                MessageBox.Show($"Error loading product name for detail (ID: {item.ProductID}): {prodEx.Message}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        orderDetails = new BindingList<OrderDetailsDTO>(details);
                    }
                    else
                    {
                        MessageBox.Show($"Order with ID {OrderID} not found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        orderDetails = new BindingList<OrderDetailsDTO>(); // Initialize empty
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading order or details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    orderDetails = new BindingList<OrderDetailsDTO>(); // Initialize empty
                }
            }
            else
            {
                orderDetails = new BindingList<OrderDetailsDTO>();
                //dtpOrderDate.Value = DateTime.Now; // Set current date for new order
            }

            dataGridView.DataSource = orderDetails;
            dataGridView.SelectionChanged += dataGridView_SelectionChanged;
            EnableControls();
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null || dataGridView.CurrentRow.DataBoundItem == null)
            {
                // Clear controls if no row is selected
                cboProduct.SelectedIndex = -1;
                numQuantity.Value = 1;
                numPrice.Value = 0;
                btnDelete.Enabled = false; // Disable delete if nothing is selected
                return;
            }

            var selectedDetail = dataGridView.CurrentRow.DataBoundItem as OrderDetailsDTO;
            if (selectedDetail != null)
            {
                // Set SelectedValue only if the value exists in the DataSource
                if (cboProduct.Items.Cast<ProductDTO>().Any(p => p.ID == selectedDetail.ProductID))
                {
                    cboProduct.SelectedValue = selectedDetail.ProductID;
                }
                else
                {
                    cboProduct.SelectedIndex = -1; // Clear selection if product not found
                }
                numQuantity.Value = selectedDetail.Quantity;
                numPrice.Value = selectedDetail.Price;
            }
            btnDelete.Enabled = true; // Enable delete if a row is selected
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cboProduct.SelectedValue == null)
            {
                MessageBox.Show("Please select product.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numQuantity.Value <= 0)
            {
                MessageBox.Show("Sales quantity must be greater than 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numPrice.Value <= 0)
            {
                MessageBox.Show("Product selling price must be greater than 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int productID = Convert.ToInt32(cboProduct.SelectedValue);
            // Find existing detail by ProductID (assuming one product per detail line)
            var existingDetail = orderDetails.FirstOrDefault(x => x.ProductID == productID);

            if (existingDetail != null)
            {
                // Update existing product detail
                existingDetail.Quantity = (short)Convert.ToInt32(numQuantity.Value);
                existingDetail.Price = Convert.ToInt32(numPrice.Value);
                // No need to set ProductName again, it's already there
                dataGridView.Refresh(); // Refresh the display
            }
            else
            {
                // Add new product detail
                var newDetail = new OrderDetailsDTO
                {
                    ID = 0, // This ID will be assigned by the server on save
                    OrderID = OrderID, // Will be updated after main Order is saved if OrderID is 0
                    ProductID = productID,
                    ProductName = cboProduct.Text, // Get name from combobox
                    Quantity = (short)numQuantity.Value,
                    Price = Convert.ToInt32(numPrice.Value),
                };
                orderDetails.Add(newDetail);
            }

            EnableControls();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null) return;

            // Get the selected OrderDetailsDTO object
            var selectedDetail = dataGridView.CurrentRow.DataBoundItem as OrderDetailsDTO;
            if (selectedDetail != null)
            {
                orderDetails.Remove(selectedDetail);
            }
            EnableControls();
        }

        private async void cboProduct_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboProduct.SelectedValue == null)
            {
                numPrice.Value = 0; // Clear price if no product selected
                return;
            }   

            int productID = Convert.ToInt32(cboProduct.SelectedValue);
            try
            {
                var product = await _clientService.GetProductByIdAsync(productID);
                if (product != null)
                {
                    numPrice.Value = product.Price;
                }
                else
                {
                    MessageBox.Show($"Product with ID {productID} not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    numPrice.Value = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting product price: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                numPrice.Value = 0;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (cboEmployee.SelectedValue == null)
            {
                MessageBox.Show("Please select billing staff.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboCustomer.SelectedValue == null)
            {
                MessageBox.Show("Please select customer.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (orderDetails.Count == 0)
            {
                MessageBox.Show("Please add at least one product to the order details.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var orderDto = new OrderDTO
            {
                ID = OrderID,
                EmployeeID = Convert.ToInt32(cboEmployee.SelectedValue),
                EmployeeName = cboEmployee.Text,
                CustomerID = Convert.ToInt32(cboCustomer.SelectedValue),
                CustomerName = cboCustomer.Text,
                Date = DateTime.Now.Date,
                Note = txtNote.Text,
                TotalPrice = orderDetails.Sum(d => (decimal)d.TotalPrice)
            };

            var orderWithDetailsDto = new OrderWithDetailsDTO
            {
                Order = orderDto,
                OrderDetails = orderDetails.ToList()
            };

            string message = "";
            bool operationSuccess = false;

            try
            {
                if (OrderID != 0) // Cập nhật đơn hàng
                {
                    cboEmployee.Enabled = false;

                    operationSuccess = await _clientService.UpdateOrderWithDetailsAsync(orderWithDetailsDto);
                    if (operationSuccess)
                    {
                        message = "Order updated successfully!";                    }
                    else
                    {
                        message = "Failed to create order. Server did not return a valid order ID.";
                    }
                }
                else // Tạo đơn hàng mới
                {
                    cboEmployee.Enabled = true;

                    int newOrderId = await _clientService.CreateOrderAsync(orderWithDetailsDto);
                    if (newOrderId > 0)
                    {
                        OrderID = newOrderId;
                        message = $"Order created successfully with ID: {OrderID}!";
                        operationSuccess = true; // Đánh dấu thành công
                    }
                    else
                    {
                        message = "Failed to create order. Server did not return a valid order ID.";
                    }
                }
            }
            catch (Exception ex)
            {
                string fullMessage = ex.Message;
                if (ex.InnerException != null)
                    fullMessage += "\n\nInner Exception: " + ex.InnerException.Message;

                MessageBox.Show($"An error occurred while saving the order:\n{fullMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Thoát ngay nếu có lỗi trong quá trình giao tiếp
            }
            finally
            {
                // Đảm bảo UI trở lại trạng thái bình thường (nếu bạn đã enable/disable các control)
                // Cursor.Current = Cursors.Default;
                // btnSave.Enabled = true;
                btnSave.Enabled = true;
            }

            // Hiển thị MessageBox sau khi đảm bảo thao tác mạng đã hoàn tất
            if (operationSuccess)
            {
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK; // Đặt DialogResult sau khi thông báo
                this.Close(); // Đóng form sau khi thông báo và đặt DialogResult
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            int id = OrderID;
            using (frmPrintOrder printOrder = new frmPrintOrder(id, _clientService))
            {
                printOrder.ShowDialog();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Nothing specific here based on your original code
        }
    }
}