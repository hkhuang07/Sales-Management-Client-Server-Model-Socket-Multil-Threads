// ElectronicsStore.Presentation/frmProducts.cs
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
using SlugGenerator; // Đảm bảo đã thêm thư viện này hoặc tự implement GenerateSlug
using ElectronicsStore.Client; // Assuming this contains your ClientService and related DTOs

namespace ElectronicsStore.Presentation
{
    public partial class frmProducts : Form
    {
        private bool signAdd = false;
        private int id; // Product ID for update/delete
        // imagesFolder will be the local cache directory for images
        string imagesFolder = Path.Combine(Application.StartupPath, "Images");
        BindingSource binding = new BindingSource();
        private readonly ClientService _clientService;

        // Variables to store the byte data and original filename of the selected image
        private byte[] _selectedImageBytes = null;
        private string _selectedImageFileName = null; // Original file name of the selected image

        public frmProducts(ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();

            string helpURL = ConfigurationManager.AppSettings["HelpURL"]!.ToString();
            helpProvider1.HelpNamespace = helpURL + "products.html";

            // Ensure the images folder exists to cache images
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            // Create a default image file if it doesn't exist
            string defaultImagePath = Path.Combine(imagesFolder, "product_default.jpg");
            if (!File.Exists(defaultImagePath))
            {
                // You might want to embed a default image in your resources
                // or provide a placeholder image with your application.
                // For demonstration, let's create a simple red square.
                using (Bitmap bmp = new Bitmap(100, 100))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.FillRectangle(Brushes.Red, 0, 0, 100, 100);
                        g.DrawString("No Image", new Font("Arial", 10), Brushes.White, 10, 40);
                    }
                    bmp.Save(defaultImagePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
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
            // picImage.Enabled = value; // PictureBox itself is always enabled for display
            btnChangeImage.Enabled = !value;

            btnAdd.Enabled = !value;
            btnUpdate.Enabled = !value;
            btnDelete.Enabled = !value;
            btnFind.Enabled = !value;
            btnImport.Enabled = !value;
            btnExport.Enabled = !value;
            btnClear.Enabled = !value;
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
            btnFind.Click += async (s, e) =>
            {
                string keyword = txtFind.Text.Trim();
                try
                {
                    List<ProductDTO> filteredProducts;
                    if (string.IsNullOrEmpty(keyword))
                    {
                        filteredProducts = await _clientService.GetAllProductsAsync();
                    }
                    else
                    {
                        filteredProducts = await _clientService.SearchProductsAsync(keyword);
                    }

                    binding.DataSource = filteredProducts;
                    dataGridView.DataSource = binding; // Re-assign DataSource
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
            _selectedImageBytes = null; // Clear selected image data on data load
            _selectedImageFileName = null;

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
                picImage.DataBindings.Clear(); // Very important: Clear old binding for picImage

                // Add new bindings for other controls
                txtProductName.DataBindings.Add("Text", binding, "ProductName", false, DataSourceUpdateMode.Never);
                txtDescription.DataBindings.Add("Text", binding, "Description", false, DataSourceUpdateMode.Never);
                numPrice.DataBindings.Add("Value", binding, "Price", false, DataSourceUpdateMode.Never);
                numQuantity.DataBindings.Add("Value", binding, "Quantity", false, DataSourceUpdateMode.Never);
                cboCategory.DataBindings.Add("SelectedValue", binding, "CategoryID", false, DataSourceUpdateMode.Never);
                cboManufacturer.DataBindings.Add("SelectedValue", binding, "ManufacturerID", false, DataSourceUpdateMode.Never);

                dataGridView.DataSource = binding; // Assign DataSource to DataGridView from BindingSource

                // Handle displaying images for PictureBox when selected product changes
                // Remove old event to avoid multiple subscriptions
                binding.CurrentChanged -= Binding_CurrentChanged;
                binding.CurrentChanged += Binding_CurrentChanged;

                // Call for the first time to display the image of the first product
                Binding_CurrentChanged(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // New method to handle image for PictureBox (product details)
        private async void Binding_CurrentChanged(object sender, EventArgs e)
        {
            if (binding.Current is ProductDTO currentProduct)
            {
                // Use the image filename from DTO, if empty then use default image
                string fileName = string.IsNullOrEmpty(currentProduct.Image) ? "product_default.jpg" : currentProduct.Image;
                string fullPath = Path.Combine(imagesFolder, fileName);

                // Dispose old image before loading new one to avoid GDI+ errors and memory leaks
                if (picImage.Image != null)
                {
                    picImage.Image.Dispose();
                    picImage.Image = null;
                }

                if (File.Exists(fullPath)) // Check in local cache
                {
                    try
                    {
                        // Load image from file by creating a copy to release file handle immediately
                        using (var tempImage = System.Drawing.Image.FromFile(fullPath))
                        {
                            picImage.Image = new Bitmap(tempImage);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading image from cache '{fullPath}': {ex.Message}");
                        // Fallback to default if cached image is corrupted
                        using (var defaultImage = System.Drawing.Image.FromFile(Path.Combine(imagesFolder, "product_default.jpg")))
                        {
                            picImage.Image = new Bitmap(defaultImage);
                        }
                    }
                }
                else // If not in cache, download from server
                {
                    try
                    {
                        // Check if this method exists in ClientService
                        // If not, you need to create it to download image as byte array
                        byte[] imageData = await _clientService.GetProductImageAsync(fileName);
                        if (imageData != null && imageData.Length > 0)
                        {
                            await File.WriteAllBytesAsync(fullPath, imageData); // Save to cache
                            using (MemoryStream ms = new MemoryStream(imageData))
                            {
                                picImage.Image = System.Drawing.Image.FromStream(ms);
                            }
                        }
                        else
                        {
                            // Fallback to default if image data is empty or null
                            using (var defaultImage = System.Drawing.Image.FromFile(Path.Combine(imagesFolder, "product_default.jpg")))
                            {
                                picImage.Image = new Bitmap(defaultImage);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error downloading image '{fileName}': {ex.Message}");
                        // Fallback to default image on download error
                        using (var defaultImage = System.Drawing.Image.FromFile(Path.Combine(imagesFolder, "product_default.jpg")))
                        {
                            picImage.Image = new Bitmap(defaultImage);
                        }
                    }
                }
            }
            else
            {
                // Clear image if no product is selected or currentProduct is null
                if (picImage.Image != null)
                {
                    picImage.Image.Dispose();
                    picImage.Image = null;
                }
                // Display default image if no product is selected
                using (var defaultImage = System.Drawing.Image.FromFile(Path.Combine(imagesFolder, "product_default.jpg")))
                {
                    picImage.Image = new Bitmap(defaultImage);
                }
            }
        }

        private async void frmProducts_Load(object sender, EventArgs e)
        {
            await LoadProductData();
        }

        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Only process the image column if it exists and is the column we are interested in
            if (dataGridView.Columns[e.ColumnIndex].Name == "Image") // Ensure column name is "ImageColumn"
            {
                string fileName = string.IsNullOrEmpty(e.Value?.ToString()) ? "product_default.jpg" : e.Value.ToString();
                string fullPath = Path.Combine(imagesFolder, fileName);

                System.Drawing.Image imageToDisplay = null;

                try
                {
                    if (File.Exists(fullPath)) // Check if image is already in local cache
                    {
                        // Load image from file and create a small Bitmap copy for display
                        // This is important to avoid holding file locks and optimize memory
                        using (var originalImage = System.Drawing.Image.FromFile(fullPath))
                        {
                            imageToDisplay = new Bitmap(originalImage, 24, 24); // Fixed size for DGV
                        }
                    }
                    else
                    {
                        // If image is not in local cache, display default image
                        // DO NOT download image from server here!
                        using (var defaultImage = System.Drawing.Image.FromFile(Path.Combine(imagesFolder, "product_default.jpg")))
                        {
                            imageToDisplay = new Bitmap(defaultImage, 24, 24);
                        }
                    }
                    e.Value = imageToDisplay;
                    e.FormattingApplied = true; // Mark that formatting has been applied
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error formatting image in DataGridView for file '{fileName}': {ex.Message}");
                    // Fallback to default image silently on error
                    try
                    {
                        using (var defaultImage = System.Drawing.Image.FromFile(Path.Combine(imagesFolder, "product_default.jpg")))
                        {
                            e.Value = new Bitmap(defaultImage, 24, 24);
                        }
                        e.FormattingApplied = true;
                    }
                    catch { /* ignore, to prevent infinite loops or deeper errors */ }
                }
                finally
                {
                    // Do not dispose imageToDisplay here as it's assigned to e.Value and DGV will manage it.
                    // The Bitmap copy ensures no original resource is held.
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            signAdd = true;
            EnableControls(true);
            cboCategory.Text = ""; // Clear selected category
            cboManufacturer.Text = ""; // Clear selected manufacturer
            txtProductName.Clear();
            txtDescription.Clear();
            numPrice.Value = 0;
            numQuantity.Value = 0;
            if (picImage.Image != null)
            {
                picImage.Image.Dispose();
                picImage.Image = null; // Clear image
            }
            // Display default image when adding
            picImage.Image = System.Drawing.Image.FromFile(Path.Combine(imagesFolder, "product_default.jpg"));
            _selectedImageBytes = null; // Clear selected image data for new entry
            _selectedImageFileName = null;
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
                    // When updating, no need to set _selectedImageBytes as the current image might not be changed.
                    // If the user selects a new image, _selectedImageBytes will be updated in btnChangeImage_Click.
                    _selectedImageBytes = null;
                    _selectedImageFileName = null;
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
                        await LoadProductData();
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

        // Event handler for the "Change Image" button
        private void btnChangeImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Read the selected image file into a byte array
                    _selectedImageBytes = File.ReadAllBytes(openFileDialog.FileName);
                    _selectedImageFileName = Path.GetFileName(openFileDialog.FileName);

                    // Display the selected image in the PictureBox
                    using (MemoryStream ms = new MemoryStream(_selectedImageBytes))
                    {
                        if (picImage.Image != null)
                        {
                            picImage.Image.Dispose();
                        }
                        picImage.Image = System.Drawing.Image.FromStream(ms);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _selectedImageBytes = null;
                    _selectedImageFileName = null;
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

                ProductDTO dto = new ProductDTO
                {
                    ProductName = txtProductName.Text,
                    Description = txtDescription.Text,
                    Price = (int)numPrice.Value,
                    Quantity = (int)numQuantity.Value,
                    CategoryID = Convert.ToInt32(cboCategory.SelectedValue),
                    ManufacturerID = Convert.ToInt32(cboManufacturer.SelectedValue)
                };

                // Determine the image filename to send with the DTO for initial save/update
                if (_selectedImageFileName != null && _selectedImageBytes != null)
                {
                    // If a new image is selected, generate a slugged filename for the DTO
                    // The server will handle saving and associating this file.
                    dto.Image = _selectedImageFileName.GenerateSlug() + Path.GetExtension(_selectedImageFileName);
                }
                else if (!signAdd && binding.Current is ProductDTO currentBoundProduct)
                {
                    // If updating and no new image is selected, retain the existing image filename
                    dto.Image = currentBoundProduct.Image;
                }
                else
                {
                    // For new products with no image selected, or if existing product has no image
                    dto.Image = "product_default.jpg";
                }


                ProductDTO resultProduct;
                if (signAdd)
                {
                    resultProduct = await _clientService.AddProductAsync(dto);
                    id = resultProduct.ID; // Get the new ID to upload the image
                    MessageBox.Show("Product added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dto.ID = id; // Ensure ID is set for update operation
                    resultProduct = await _clientService.UpdateProductAsync(dto);
                    MessageBox.Show("Product updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // Handle image upload to server if a new image was selected
                if (_selectedImageBytes != null && _selectedImageBytes.Length > 0 && !string.IsNullOrEmpty(_selectedImageFileName))
                {
                    try
                    {
                        // The server will save the image and ensure the Product.Image field is updated
                        // with the correct filename (the slugged version).
                        bool uploadSuccess = await _clientService.UploadProductImageAsync(resultProduct.ID, dto.Image, _selectedImageBytes);

                        if (!uploadSuccess)
                        {
                            MessageBox.Show("Failed to upload product image to server. Product details saved.", "Image Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception imageUploadEx)
                    {
                        MessageBox.Show($"Error uploading image: {imageUploadEx.Message}. Product details saved.", "Image Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // Important: clear the selected image data after saving/uploading
                _selectedImageBytes = null;
                _selectedImageFileName = null;


                await LoadProductData(); // Reload data to update image display
                EnableControls(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server or processing request: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            await LoadProductData();
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
                await LoadProductData();
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
                            else
                            {
                                product.Image = "product_default.jpg"; // Set default if not specified in Excel
                            }

                            productsToImport.Add(product);
                        }
                    }

                    if (productsToImport.Any())
                    {
                        bool success = await _clientService.BulkAddProductsAsync(productsToImport);
                        if (success)
                        {
                            MessageBox.Show($"Successfully imported {productsToImport.Count} products.", "Import Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadProductData();
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
                        worksheet.Cell(1, 8).Value = "CategoryID"; // Added CategoryID to export
                        worksheet.Cell(1, 9).Value = "CategoryName"; // Added CategoryName to export
                        worksheet.Cell(1, 10).Value = "ManufacturerName"; // Added ManufacturerName to export

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
                                worksheet.Cell(rowNum, 8).Value = product.CategoryID;
                                worksheet.Cell(rowNum, 9).Value = product.CategoryName;
                                worksheet.Cell(rowNum, 10).Value = product.ManufacturerName;
                                rowNum++;
                            }
                            workbook.SaveAs(saveFileDialog.FileName);
                            MessageBox.Show("Data exported successfully to Excel.", "Export Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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