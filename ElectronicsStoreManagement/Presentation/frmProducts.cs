using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using ElectronicsStore.DataTransferObject;
using Newtonsoft.Json;
using SlugGenerator;
using ElectronicsStore.Client;

namespace ElectronicsStore.Presentation
{
    public partial class frmProducts : Form
    {
        private bool signAdd = false;
        private int id; // Product ID for update/delete
        string imagesFolder = Path.Combine(Application.StartupPath, "Images");
        BindingSource binding = new BindingSource();
        private readonly ClientService _clientService;

        public frmProducts(ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();
            //_clientService = clientService; // ClientService is injected

            string helpURL = ConfigurationManager.AppSettings["HelpURL"]!.ToString();
            helpProvider1.HelpNamespace = helpURL + "products.html";

            // Ensure images folder exists
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }
        }

        private void EnableControls(bool value)
        {
            btnSave.Enabled = value;
            btnCancel.Enabled = value;
            txtProductName.Enabled = value;
            txtDescription.Enabled = value;
            numPrice.Enabled = value;
            numQuantity.Enabled = value;
            cboCategory.Enabled = value;
            cboManufacturer.Enabled = value;
            picImage.Enabled = value;

            btnAdd.Enabled = !value;
            btnUpdate.Enabled = !value;
            btnDelete.Enabled = !value;
            btnChangeImage.Enabled = !value;
            btnFind.Enabled = !value; // Keep find enabled for manual search trigger
            btnImport.Enabled = !value;
            btnExport.Enabled = !value;
            btnClear.Enabled = !value; // Clear search button
            txtFind.Enabled = !value;
        }

        public async Task GetCategories()
        {
            try
            {
                List<CategoryDTO> categories = await _clientService.GetAllCategoriesAsync();
                cboCategory.DataSource = categories;
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async Task GetManufacturers()
        {
            try
            {
                List<ManufacturerDTO> manufacturers = await _clientService.GetAllManufacturersAsync();
                cboManufacturer.DataSource = manufacturers;
                cboManufacturer.DisplayMember = "ManufacturerName";
                cboManufacturer.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading manufacturers: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupToolStrip()
        {
            // Move to first
            btnBegin.Click += (s, e) =>
            {
                if (binding.Count > 0)
                    binding.MoveFirst();
            };

            // Move to previous
            btnPrevious.Click += (s, e) =>
            {
                if (binding.Position > 0)
                    binding.MovePrevious();
            };

            // Move to next
            btnNext.Click += (s, e) =>
            {
                if (binding.Position < binding.Count - 1)
                    binding.MoveNext();
            };

            // Move to last
            btnEnd.Click += (s, e) =>
            {
                if (binding.Count > 0)
                    binding.MoveLast();
            };

            // Search button
            btnFind.Click += async (s, e) => // Keep this async as it calls async methods
            {
                string keyword = txtFind.Text.Trim();
                try
                {
                    List<ProductDTO> filteredProducts;
                    if (string.IsNullOrEmpty(keyword))
                    {
                        // If search box is empty, load all data
                        filteredProducts = await _clientService.GetAllProductsAsync();
                    }
                    else
                    {
                        // Call server to search products by keyword
                        filteredProducts = await _clientService.SearchProductsAsync(keyword);
                    }

                    binding.DataSource = filteredProducts;
                    dataGridView.DataSource = binding;
                    if (filteredProducts == null || filteredProducts.Count == 0)
                    {
                        lblMessage.Text = "No matching product found.";
                    }
                    else
                    {
                        lblMessage.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error connecting to server or searching products: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            txtFind.TextChanged += (s, e) =>
            {
                lblMessage.Text = ""; // Clear the message label when text changes
            };
        }

        /// <summary>
        /// Loads product data, categories, and manufacturers into the form controls.
        /// This method is designed to be called whenever the data needs to be refreshed.
        /// </summary>
        private async Task LoadProductData()
        {
            dataGridView.AutoGenerateColumns = false;
            EnableControls(false);

            await GetCategories();
            await GetManufacturers();

            try
            {
                List<ProductDTO> list = await _clientService.GetAllProductsAsync();
                binding.DataSource = list;
                SetupToolStrip();

                // Clear existing bindings to prevent issues when reloading
                txtProductName.DataBindings.Clear();
                txtDescription.DataBindings.Clear();
                numPrice.DataBindings.Clear();
                numQuantity.DataBindings.Clear();
                cboCategory.DataBindings.Clear();
                cboManufacturer.DataBindings.Clear();
                picImage.DataBindings.Clear();

                // Add new bindings
                txtProductName.DataBindings.Add("Text", binding, "ProductName", false, DataSourceUpdateMode.Never);
                txtDescription.DataBindings.Add("Text", binding, "Description", false, DataSourceUpdateMode.Never);
                numPrice.DataBindings.Add("Value", binding, "Price", false, DataSourceUpdateMode.Never);
                numQuantity.DataBindings.Add("Value", binding, "Quantity", false, DataSourceUpdateMode.Never);
                cboCategory.DataBindings.Add("SelectedValue", binding, "CategoryID", false, DataSourceUpdateMode.Never);
                cboManufacturer.DataBindings.Add("SelectedValue", binding, "ManufacturerID", false, DataSourceUpdateMode.Never);

                Binding imageBinding = new Binding("ImageLocation", binding, "Image");
                imageBinding.Format += (s, args) =>
                {
                    string fileName = string.IsNullOrEmpty(args.Value?.ToString()) ? "product_default.jpg" : args.Value.ToString();
                    args.Value = Path.Combine(imagesFolder, fileName);
                };
                picImage.DataBindings.Add(imageBinding);

                dataGridView.DataSource = binding;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void frmProducts_Load(object sender, EventArgs e)
        {
            await LoadProductData();
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dataGridView.Columns[e.ColumnIndex].Name == "ImageColumn")
                {
                    string fileName = string.IsNullOrEmpty(e.Value?.ToString()) ? "product_default.jpg" : e.Value.ToString();
                    string fullPath = Path.Combine(imagesFolder, fileName);

                    System.Drawing.Image imageToShow;
                    if (File.Exists(fullPath))
                    {
                        imageToShow = System.Drawing.Image.FromFile(fullPath);
                    }
                    else
                    {
                        imageToShow = System.Drawing.Image.FromFile(Path.Combine(imagesFolder, "product_default.jpg"));
                    }

                    imageToShow = new Bitmap(imageToShow, 24, 24);
                    e.Value = imageToShow;
                }
            }
            catch (Exception ex)
            {
                // Consider logging this or setting a default image silently.
                // MessageBox.Show($"Error formatting image: {ex.Message}", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            signAdd = true;
            EnableControls(true);
            cboCategory.Text = "";
            cboManufacturer.Text = "";
            txtProductName.Clear();
            txtDescription.Clear();
            numPrice.Value = 0;
            numQuantity.Value = 0;
            picImage.Image = null; // Clear image
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            signAdd = false;
            if (dataGridView.CurrentRow != null)
            {
                EnableControls(true);
                if (dataGridView.CurrentRow.Cells["ID"].Value != null && int.TryParse(dataGridView.CurrentRow.Cells["ID"].Value.ToString(), out int parsedId))
                {
                    id = parsedId;
                }
                else
                {
                    MessageBox.Show("Selected row does not have a valid Product ID.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    EnableControls(false);
                }
            }
            else
            {
                MessageBox.Show("Please select a product to update.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                EnableControls(false);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Please select a product to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this product?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int idToDelete = Convert.ToInt32(dataGridView.CurrentRow.Cells["ID"].Value.ToString());
                    bool success = await _clientService.DeleteProductAsync(idToDelete);

                    if (success)
                    {
                        MessageBox.Show("Product deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadProductData(); // Call the new refresh method
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete product.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error connecting to server or processing request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Basic validation
                if (string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    MessageBox.Show("Product Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cboCategory.SelectedValue == null)
                {
                    MessageBox.Show("Please select a Category.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cboManufacturer.SelectedValue == null)
                {
                    MessageBox.Show("Please select a Manufacturer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dto = new ProductDTO
                {
                    ProductName = txtProductName.Text,
                    Description = txtDescription.Text,
                    Price = (int)numPrice.Value,
                    Quantity = (int)numQuantity.Value,
                    CategoryID = Convert.ToInt32(cboCategory.SelectedValue),
                    ManufacturerID = Convert.ToInt32(cboManufacturer.SelectedValue),
                    Image = (picImage.ImageLocation != null) ? Path.GetFileName(picImage.ImageLocation) : "product_default.jpg"
                };

                ProductDTO resultProduct;
                if (signAdd)
                {
                    resultProduct = await _clientService.AddProductAsync(dto);
                }
                else
                {
                    dto.ID = id; // Ensure ID is set for update
                    resultProduct = await _clientService.UpdateProductAsync(dto);
                }

                MessageBox.Show("Operation completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadProductData(); // Call the new refresh method
                EnableControls(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server or processing request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            await LoadProductData(); // Call the new refresh method
            EnableControls(false);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private async void btnClear_Click(object sender, EventArgs e)
        {
            txtFind.Clear();
            try
            {
                await LoadProductData(); // Call the new refresh method
                lblMessage.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    List<ProductDTO> productsToImport = new List<ProductDTO>();
                    using (XLWorkbook workbook = new XLWorkbook(openFileDialog.FileName))
                    {
                        IXLWorksheet worksheet = workbook.Worksheet(1);
                        bool firstRow = true;
                        Dictionary<string, int> columnIndexes = new Dictionary<string, int>();

                        foreach (IXLRow row in worksheet.RowsUsed())
                        {
                            if (firstRow)
                            {
                                int colIndex = 1;
                                foreach (IXLCell cell in row.CellsUsed())
                                {
                                    columnIndexes[cell.Value.ToString().Trim()] = colIndex;
                                    colIndex++;
                                }
                                firstRow = false;
                                if (!columnIndexes.ContainsKey("ProductName") ||
                                    !columnIndexes.ContainsKey("Description") ||
                                    !columnIndexes.ContainsKey("Price") ||
                                    !columnIndexes.ContainsKey("Quantity") ||
                                    !columnIndexes.ContainsKey("CategoryID") ||
                                    !columnIndexes.ContainsKey("ManufacturerID"))
                                {
                                    MessageBox.Show("The Excel file must contain 'ProductName', 'Description', 'Price', 'Quantity', 'CategoryID', and 'ManufacturerID' columns.", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                continue;
                            }

                            ProductDTO product = new ProductDTO();
                            if (columnIndexes.TryGetValue("ProductName", out int prodNameCol))
                                product.ProductName = row.Cell(prodNameCol).GetValue<string>();
                            if (columnIndexes.TryGetValue("Description", out int descCol))
                                product.Description = row.Cell(descCol).GetValue<string>();
                            if (columnIndexes.TryGetValue("Price", out int priceCol))
                                product.Price = row.Cell(priceCol).GetValue<int>();
                            if (columnIndexes.TryGetValue("Quantity", out int qtyCol))
                                product.Quantity = row.Cell(qtyCol).GetValue<int>();
                            if (columnIndexes.TryGetValue("CategoryID", out int catIdCol))
                                product.CategoryID = row.Cell(catIdCol).GetValue<int>();
                            if (columnIndexes.TryGetValue("ManufacturerID", out int manIdCol))
                                product.ManufacturerID = row.Cell(manIdCol).GetValue<int>();
                            if (columnIndexes.TryGetValue("Image", out int imageCol))
                                product.Image = row.Cell(imageCol).GetValue<string>();

                            productsToImport.Add(product);
                        }
                    }

                    if (productsToImport.Any())
                    {
                        // FIX 1: Ensure BulkAddProductsAsync returns a bool (or handle its return type appropriately)
                        bool success = await _clientService.BulkAddProductsAsync(productsToImport);
                        if (success)
                        {
                            MessageBox.Show($"Successfully imported {productsToImport.Count} products.", "Import Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadProductData(); // Call the new refresh method
                        }
                        else
                        {
                            MessageBox.Show("Failed to import products. Please check the server logs for details.", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No products found in the Excel file to import.", "Import Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error importing data: {ex.Message}", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (binding.Count == 0)
            {
                MessageBox.Show("No data to export.", "Export Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export data to Excel file";
            saveFileDialog.Filter = "Excel file|*.xlsx";
            saveFileDialog.FileName = "ProductsExport_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xlsx";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Products");

                        // Add headers
                        worksheet.Cell(1, 1).Value = "ID";
                        worksheet.Cell(1, 2).Value = "ProductName";
                        worksheet.Cell(1, 3).Value = "Price";
                        worksheet.Cell(1, 4).Value = "Quantity";
                        worksheet.Cell(1, 5).Value = "Image";
                        worksheet.Cell(1, 6).Value = "Description";
                        worksheet.Cell(1, 7).Value = "ManufacturerID";
                        // Add other headers as needed.

                        // Get the data from the BindingSource
                        var products = binding.DataSource as List<ProductDTO>;

                        if (products != null)
                        {
                            int rowNum = 2; // Start from row 2 for data
                            foreach (var product in products)
                            {
                                worksheet.Cell(rowNum, 1).Value = product.ID;
                                worksheet.Cell(rowNum, 2).Value = product.ProductName;
                                worksheet.Cell(rowNum, 3).Value = product.Price;
                                worksheet.Cell(rowNum, 4).Value = product.Quantity;
                                worksheet.Cell(rowNum, 5).Value = product.Image;
                                worksheet.Cell(rowNum, 6).Value = product.Description;
                                worksheet.Cell(rowNum, 7).Value = product.ManufacturerID;
                                // Populate other cells based on your ProductDTO properties
                                rowNum++;
                            }

                            workbook.SaveAs(saveFileDialog.FileName);
                            MessageBox.Show("Data exported successfully!", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Could not retrieve product data for export.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error exporting data: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}