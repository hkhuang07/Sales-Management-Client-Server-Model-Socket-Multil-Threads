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
using ElectronicsStore.DataTransferObject;
using ElectronicsStore.Client;
using System.IO;

namespace ElectronicsStore.Presentation
{
    public partial class frmSale : Form
    {
        private int currentOrderID = 0;
        private List<OrderDetailsDTO> orderDetails = new List<OrderDetailsDTO>();
        string imagesFolder = Path.Combine(Application.StartupPath, "Images");

        BindingSource bindingOrder = new BindingSource();
        BindingSource bindingOrderDetails = new BindingSource();

        public readonly ClientService _clientService;

        public frmSale(ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
            string helpURL = ConfigurationManager.AppSettings["HelpURL"]!.ToString();
            helpProvider1.HelpNamespace = helpURL + "sale.html";
        }


        // Product and Order Loading

        /// <summary>
        /// Loads products from the server and displays them as cards.
        /// </summary>
        private async Task LoadProductsAsync()
        {
            flowLayoutPanel1.Controls.Clear();
            try
            {
                List<ProductDTO> products = await _clientService.GetAllProductsAsync();

                if (products != null)
                {
                    foreach (var product in products)
                    {
                        string fileName = string.IsNullOrEmpty(product.Image) ? "product_default.jpg" : product.Image;
                        var card = new ProductCard();
                        card.ProductName = product.ProductName;
                        card.Price = product.Price.ToString("N0");

                        string imagePath = Path.Combine(imagesFolder, fileName);
                        if (File.Exists(imagePath))
                        {
                            card.ProductImage = Image.FromFile(imagePath);
                        }
                        else
                        {               
                            string defaultImagePath = Path.Combine(imagesFolder, "product_default.jpg");
                            if (File.Exists(defaultImagePath))
                            {
                                card.ProductImage = Image.FromFile(defaultImagePath);
                            }
                            else
                            {
                                card.ProductImage = new Bitmap(100, 100);
                            }
                        }

                        card.ProductData = product;
                        card.AddClicked += ProductCard_AddToOrder;
                        card.SubtractClicked += ProductCard_DeleteToOrder;
                        card.CardDoubleClicked += SelectCard;

                        flowLayoutPanel1.Controls.Add(card);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Loads all orders from the server and updates the DataGridView.
        /// </summary>
        private async Task LoadOrdersAsync()
        {
            try
            {
                List<OrderDTO> orderList = await _clientService.GetAllOrdersAsync();
                bindingOrder.DataSource = orderList;
                dgvOrder.DataSource = bindingOrder;
                UpdateRevenue();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the form load event, initializing product and order data.
        /// </summary>
        private async void Saller_Load(object sender, EventArgs e)
        {
            await LoadProductsAsync();
            dgvOrder.AutoGenerateColumns = false;
            dgvOrderDetails.AutoGenerateColumns = false;
            await LoadOrdersAsync(); // Call the new async method to load orders
        }


        // Search and Filter

        private async void btnFind_Click(object sender, EventArgs e)
        {
            string keyword = txtFind.Text.Trim();
            flowLayoutPanel1.Controls.Clear();

            try
            {
                List<ProductDTO> filteredProducts;
                if (string.IsNullOrEmpty(keyword))
                {
                    await LoadProductsAsync();
                    return;
                }
                else
                {
                    filteredProducts = await _clientService.SendRequest<string, List<ProductDTO>>("SearchProducts", keyword); // Changed method name

                }

                if (filteredProducts != null && filteredProducts.Any())
                {
                    foreach (var product in filteredProducts)
                    {
                        string fileName = string.IsNullOrEmpty(product.Image) ? "product_default.jpg" : product.Image;
                        var card = new ProductCard();
                        card.ProductName = product.ProductName;
                        card.Price = product.Price.ToString("N0");

                        string imagePath = Path.Combine(imagesFolder, fileName);
                        if (File.Exists(imagePath))
                        {
                            card.ProductImage = Image.FromFile(imagePath);
                        }
                        else
                        {
                            string defaultImagePath = Path.Combine(imagesFolder, "product_default.jpg");
                            if (File.Exists(defaultImagePath))
                            {
                                card.ProductImage = Image.FromFile(defaultImagePath);
                            }
                            else
                            {
                                card.ProductImage = new Bitmap(100, 100);
                            }
                        }
                        card.ProductData = product;
                        card.AddClicked += ProductCard_AddToOrder;
                        card.SubtractClicked += ProductCard_DeleteToOrder;
                        card.CardDoubleClicked += SelectCard;

                        flowLayoutPanel1.Controls.Add(card);
                    }
                }
                else
                {
                    MessageBox.Show("No matching product found!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            btnFind_Click(sender, e);
        }

        

        // Order Details Management

        private void UpdateRevenue()
        {
            decimal totalRevenue = 0;

            foreach (DataGridViewRow row in dgvOrder.Rows)
            {
                if (row.Cells["TotalPrice"].Value != null &&
                    decimal.TryParse(row.Cells["TotalPrice"].Value.ToString(), out decimal price))
                {
                    totalRevenue += price;
                }
            }

            txtRevenue.Text = totalRevenue.ToString("N0");
        }

        private void RefreshOrderDetails()
        {
            dgvOrderDetails.DataSource = null;
            dgvOrderDetails.DataSource = orderDetails;

            decimal total = orderDetails.Sum(x => x.Price * x.Quantity);
            txtTotalDetails.Text = total.ToString("N0");
        }

        

        // Product Card Interactions

        private void SelectCard(object sender, EventArgs e)
        {
            var card = sender as ProductCard;
            if (card != null)
            {
                foreach (ProductCard pc in flowLayoutPanel1.Controls.OfType<ProductCard>())
                {
                    pc.BackColor = System.Drawing.Color.Lavender;
                }
                card.BackColor = System.Drawing.Color.CornflowerBlue;
            }
        }

        private void ProductCard_AddToOrder(object sender, EventArgs e)
        {
            var card = sender as ProductCard;
            if (card == null || card.ProductData == null)
                return;

            var product = card.ProductData;

            if (product.Price <= 0)
            {
                MessageBox.Show("Product selling price must be greater than 0.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existingDetail = orderDetails.FirstOrDefault(x => x.ProductID == product.ID);

            if (existingDetail != null)
            {
                existingDetail.Quantity++;
            }
            else
            {
                var detail = new OrderDetailsDTO
                {
                    ID = 0,
                    OrderID = currentOrderID,
                    ProductID = product.ID,
                    ProductName = product.ProductName,
                    Quantity = 1,
                    Price = product.Price
                };
                orderDetails.Add(detail);
            }

            RefreshOrderDetails();
        }

        private void ProductCard_DeleteToOrder(object sender, EventArgs e)
        {
            var card = sender as ProductCard;
            if (card == null || card.ProductData == null)
                return;

            var product = card.ProductData;

            var existingDetail = orderDetails.FirstOrDefault(x => x.ProductID == product.ID);
            if (existingDetail != null)
            {
                existingDetail.Quantity--;

                if (existingDetail.Quantity <= 0)
                {
                    orderDetails.Remove(existingDetail);
                }
                RefreshOrderDetails();
            }
        }


        // Order Actions (Add, Update, Delete, Cancel, Confirm)

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var selectedCard = flowLayoutPanel1.Controls
               .OfType<ProductCard>()
               .FirstOrDefault(c => c.BackColor == System.Drawing.Color.CornflowerBlue);

            if (selectedCard != null)
            {
                ProductCard_AddToOrder(selectedCard, EventArgs.Empty);
                selectedCard.BackColor = System.Drawing.Color.Lavender;
            }
            else
            {
                MessageBox.Show("Please select a product to add.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Order update functionality needs to be implemented. Select an order from the list and modify its details.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bntDelete_Click(object sender, EventArgs e)
        {
            if (dgvOrderDetails.CurrentRow == null)
            {
                MessageBox.Show("Please select a product from the order to delete.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var productId = Convert.ToInt32(dgvOrderDetails.CurrentRow.Cells["ProductID"].Value);

            var existingDetail = orderDetails.FirstOrDefault(x => x.ProductID == productId);

            if (existingDetail != null)
            {
                existingDetail.Quantity--;

                if (existingDetail.Quantity <= 0)
                {
                    orderDetails.Remove(existingDetail);
                }
                RefreshOrderDetails();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            orderDetails.Clear();
            RefreshOrderDetails();
            currentOrderID = 0;
            MessageBox.Show("Current order details cleared.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private async void btnOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrderDetails.Rows.Count <= 0)
            {
                MessageBox.Show("Please add products to the order first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (frmConfirm confirm = new frmConfirm(_clientService, currentOrderID))
            {
                await confirm.LoadDataAsync();

                if (confirm.ShowDialog() == DialogResult.OK)
                {
                    if (orderDetails == null || !orderDetails.Any())
                    {
                        MessageBox.Show("No products selected for the order!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var orderWithDetails = new OrderWithDetailsDTO
                    {
                        Order = new OrderDTO
                        {
                            ID = currentOrderID,
                            EmployeeID = confirm.cboEmployee.SelectedValue != null ? Convert.ToInt32(confirm.cboEmployee.SelectedValue) : 0,
                            CustomerID = confirm.CustomerID,
                            Date = DateTime.Now,
                            Note = confirm.txtNote.Text
                        },
                        OrderDetails = orderDetails.ToList()
                    };

                    try
                    {
                        if (currentOrderID != 0)
                        {
                            bool updated = await _clientService.UpdateOrderWithDetailsAsync(orderWithDetails);
                            if (updated)
                            {
                                MessageBox.Show("Order updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to update order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            /*OrderDTO newOrder = await _clientService.CreateOrderAsync(orderWithDetails);
                            if (newOrder != null && newOrder.ID > 0)
                            {
                                currentOrderID = newOrder.ID;
                                MessageBox.Show($"New order created successfully with ID: {currentOrderID}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Failed to create new order.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }*/
                        }

                        if (confirm.chkPrintInvoice.Checked)
                        {
                            frmPrintOrder report = new frmPrintOrder(currentOrderID,_clientService);
                            report.ShowDialog();
                        }

                        orderDetails.Clear();
                        RefreshOrderDetails();
                        currentOrderID = 0;
                        await LoadOrdersAsync(); // Call the new async method to reload orders
                    }
                    catch (Exception ex)
                    {
                        string fullMessage = ex.Message;
                        if (ex.InnerException != null)
                            fullMessage += "\n\n" + ex.InnerException.Message;
                        if (ex.InnerException?.InnerException != null)
                            fullMessage += "\n\n" + ex.InnerException.InnerException.Message;

                        MessageBox.Show($"An error occurred while saving order:\n{fullMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private async void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                List<OrderDTO> allOrders = await _clientService.GetAllOrdersAsync();
                List<OrderDTO> filteredOrders = allOrders
                    .Where(r => r.Date >= dtpStart.Value && r.Date <= dtpEnd.Value.AddDays(1).AddSeconds(-1))
                    .ToList();
                bindingOrder.DataSource = filteredOrders;
                dgvOrder.DataSource = bindingOrder;
                UpdateRevenue();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            // This event handler seems to be empty. No changes needed.
        }

        private async void dgvOrder_SelectionChanged(object sender, EventArgs e) // Made async
        {
            if (dgvOrder.CurrentRow != null)
            {
                // Ensure "OrderIDColumn" matches the DataPropertyName or Name of your Order ID column in dgvOrder
                int selectedOrderId = Convert.ToInt32(dgvOrder.CurrentRow.Cells["OrderIDColumn"].Value);
                await LoadOrderDetailsAsync(selectedOrderId); // Await the async method
                currentOrderID = selectedOrderId;
            }
        }

        private async Task LoadOrderDetailsAsync(int orderId) // Changed to async Task
        {
            try
            {
                List<OrderDetailsDTO> details = await _clientService.GetOrderDetailsByOrderIdAsync(orderId);
                if (details != null)
                {
                    orderDetails = details;
                    RefreshOrderDetails();
                }
                else
                {
                    orderDetails.Clear();
                    RefreshOrderDetails();
                    MessageBox.Show("No details found for the selected order.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading order details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                orderDetails.Clear();
                RefreshOrderDetails();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgvOrder.CurrentRow == null)
            {
                MessageBox.Show("Please select an order to print the invoice.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            int id = Convert.ToInt32(dgvOrder.CurrentRow.Cells["dataGridViewTextBoxColumn1"].Value);
           //int id = Convert.ToInt32(dgvOrder.CurrentRow.Cells["OrderIDColumn"].Value);

            using (frmPrintOrder printOrder = new frmPrintOrder(id, _clientService))
            {
                printOrder.ShowDialog();
            }
        }
    }
}