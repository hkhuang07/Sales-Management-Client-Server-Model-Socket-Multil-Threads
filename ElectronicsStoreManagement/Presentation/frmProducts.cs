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
using ElectronicsStore.Client;

namespace ElectronicsStore.Presentation
{
    public partial class frmProducts : Form
    {
        private bool signAdd = false;
        private int id; // Product ID for update/delete
        // imagesFolder giờ sẽ là thư mục cache cục bộ cho ảnh
        string imagesFolder = Path.Combine(Application.StartupPath, "Images");
        BindingSource binding = new BindingSource();
        private readonly ClientService _clientService;

        // Biến để lưu trữ dữ liệu byte của ảnh đã chọn, thay vì đường dẫn tạm thời
        private byte[] _selectedImageBytes = null;
        private string _selectedImageFileName = null; // Tên file gốc của ảnh đã chọn

        public frmProducts(ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();

            string helpURL = ConfigurationManager.AppSettings["HelpURL"]!.ToString();
            helpProvider1.HelpNamespace = helpURL + "products.html";

            // Đảm bảo thư mục images tồn tại để cache ảnh
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
            btnChangeImage.Enabled = value;

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
                picImage.DataBindings.Clear();

                // Add new bindings
                txtProductName.DataBindings.Add("Text", binding, "ProductName", false, DataSourceUpdateMode.Never);
                txtDescription.DataBindings.Add("Text", binding, "Description", false, DataSourceUpdateMode.Never);
                numPrice.DataBindings.Add("Value", binding, "Price", false, DataSourceUpdateMode.Never);
                numQuantity.DataBindings.Add("Value", binding, "Quantity", false, DataSourceUpdateMode.Never);
                cboCategory.DataBindings.Add("SelectedValue", binding, "CategoryID", false, DataSourceUpdateMode.Never);
                cboManufacturer.DataBindings.Add("SelectedValue", binding, "ManufacturerID", false, DataSourceUpdateMode.Never);

                // Binding for image
                Binding imageBinding = new Binding("Image", binding, "Image");
                imageBinding.Format += async (s, args) =>
                {
                    string fileName = string.IsNullOrEmpty(args.Value?.ToString()) ? "product_default.jpg" : args.Value.ToString();
                    string fullPath = Path.Combine(imagesFolder, fileName);

                    // If the image is not in local cache, try to download it from the server
                    if (!File.Exists(fullPath))
                    {
                        try
                        {
                            byte[] imageData = await _clientService.GetProductImageAsync(fileName);
                            if (imageData != null && imageData.Length > 0)
                            {
                                await File.WriteAllBytesAsync(fullPath, imageData);
                                // Dispose previous image if exists to prevent GDI+ errors
                                if (picImage.Image != null)
                                {
                                    picImage.Image.Dispose();
                                    picImage.Image = null;
                                }
                                args.Value = System.Drawing.Image.FromFile(fullPath);
                            }
                            else
                            {
                                // Fallback to default image if download fails or image is empty
                                fullPath = Path.Combine(imagesFolder, "product_default.jpg");
                                if (picImage.Image != null)
                                {
                                    picImage.Image.Dispose();
                                    picImage.Image = null;
                                }
                                args.Value = System.Drawing.Image.FromFile(fullPath);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error downloading image '{fileName}': {ex.Message}");
                            // Fallback to default image on download error
                            fullPath = Path.Combine(imagesFolder, "product_default.jpg");
                            if (picImage.Image != null)
                            {
                                picImage.Image.Dispose();
                                picImage.Image = null;
                            }
                            args.Value = System.Drawing.Image.FromFile(fullPath);
                        }
                    }
                    else
                    {
                        // If image already in local cache, just load it
                        if (picImage.Image != null)
                        {
                            picImage.Image.Dispose();
                            picImage.Image = null;
                        }
                        args.Value = System.Drawing.Image.FromFile(fullPath);
                    }

                    // Ensure the default image exists for fallback if somehow missing
                    if (!File.Exists(Path.Combine(imagesFolder, "product_default.jpg")))
                    {
                        // Ideally, you'd embed a default image in your resources and extract it here
                        // Or ensure it's deployed with your application.
                        // For now, let's assume it exists or the PictureBox will show nothing.
                    }
                };
                picImage.DataBindings.Add(imageBinding);

                dataGridView.DataSource = binding;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connecting to server: {ex.Message}", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // **CẦN THÊM TRONG ClientService**: Một phương thức để tải ảnh về từ server
        // Ví dụ: public async Task<byte[]> GetProductImageAsync(string fileName) { ... }
        // Để đơn giản, tôi sẽ thêm nó vào ClientService ở trên.

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
                    // Lấy tên file ảnh từ cột Image (đây là tên file mà server cung cấp)
                    string fileName = string.IsNullOrEmpty(e.Value?.ToString()) ? "product_default.jpg" : e.Value.ToString();
                    string fullPath = Path.Combine(imagesFolder, fileName);

                    System.Drawing.Image imageToShow;
                    if (File.Exists(fullPath)) // Kiểm tra xem ảnh đã có trong cache cục bộ chưa
                    {
                        imageToShow = System.Drawing.Image.FromFile(fullPath);
                    }
                    else
                    {
                        // Nếu chưa có, hiển thị ảnh mặc định và client sẽ cần một cơ chế để tải ảnh về sau.
                        // (Hoặc có thể tải ảnh về ngay tại đây nếu không làm chậm UI quá nhiều, nhưng không khuyến khích trong CellFormatting)
                        imageToShow = System.Drawing.Image.FromFile(Path.Combine(imagesFolder, "product_default.jpg"));
                        // Trong môi trường client-server, bạn sẽ cần một cơ chế để tải ảnh này về từ server nếu nó không tồn tại cục bộ.
                        // Việc này phức tạp hơn và có thể thực hiện tốt hơn ở LoadProductData hoặc khi người dùng click vào row.
                        // Để đơn giản trong ví dụ này, chúng ta chỉ hiển thị ảnh mặc định nếu không tìm thấy.
                    }

                    imageToShow = new Bitmap(imageToShow, 24, 24);
                    e.Value = imageToShow;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error formatting image in DataGridView: {ex.Message}");
                // Fallback to default image silently
                try
                {
                    e.Value = new Bitmap(System.Drawing.Image.FromFile(Path.Combine(imagesFolder, "product_default.jpg")), 24, 24);
                }
                catch { /* ignore */ }
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
                    // Khi update, không cần đặt _selectedImageBytes vì ảnh hiện tại chưa chắc đã được thay đổi.
                    // Nếu người dùng chọn ảnh mới, _selectedImageBytes sẽ được cập nhật trong btnChangeImage_Click.
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
                    // Giữ nguyên Image hiện tại của DTO nếu không có ảnh mới được chọn.
                    // Nếu có ảnh mới, nó sẽ được cập nhật riêng sau.
                    // Nếu là thêm mới và không có ảnh, sẽ mặc định là "product_default.jpg"
                    Image = (picImage.ImageLocation != null && !signAdd) ? Path.GetFileName(picImage.ImageLocation) : "product_default.jpg"
                };

                ProductDTO resultProduct;
                if (signAdd)
                {
                    resultProduct = await _clientService.AddProductAsync(dto);
                    id = resultProduct.ID; // Lấy ID mới để cập nhật ảnh
                }
                else
                {
                    dto.ID = id; // Đảm bảo ID được đặt cho thao tác cập nhật
                    resultProduct = await _clientService.UpdateProductAsync(dto);
                }

                // Xử lý tải ảnh lên server nếu có ảnh mới được chọn
                if (_selectedImageBytes != null && _selectedImageBytes.Length > 0 && !string.IsNullOrEmpty(_selectedImageFileName))
                {
                    try
                    {
                        // Server sẽ lưu ảnh và trả về tên file duy nhất.
                        // Giả định server trả về tên file mới sau khi upload.
                        // Hoặc server có thể tự động liên kết ảnh với ID sản phẩm.
                        // Nếu server trả về tên file, bạn có thể muốn cập nhật ProductDTO.Image.
                        // Hiện tại, UploadProductImageAsync trả về bool, server sẽ chịu trách nhiệm cập nhật DB.
                        bool uploadSuccess = await _clientService.UploadProductImageAsync(id, _selectedImageFileName.GenerateSlug() + Path.GetExtension(_selectedImageFileName), _selectedImageBytes);

                        if (!uploadSuccess)
                        {
                            MessageBox.Show("Failed to upload product image to server.", "Image Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Product and image updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception imageUploadEx)
                    {
                        MessageBox.Show($"Error uploading image: {imageUploadEx.Message}", "Image Upload Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Operation completed successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                await LoadProductData(); // Tải lại dữ liệu để cập nhật hiển thị ảnh
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

        // Updated method to handle changing product image
        private void btnChangeImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog.Title = "Select Product Image";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Đọc file ảnh thành byte array
                    _selectedImageBytes = File.ReadAllBytes(openFileDialog.FileName);
                    _selectedImageFileName = Path.GetFileName(openFileDialog.FileName); // Lưu tên file gốc

                    // Hiển thị ảnh đã chọn trong PictureBox
                    using (MemoryStream ms = new MemoryStream(_selectedImageBytes))
                    {
                        picImage.Image = System.Drawing.Image.FromStream(ms);
                    }
                    // Không cần gán ImageLocation vì ảnh không còn được lưu cục bộ ngay lập tức.
                    // picImage.ImageLocation = null; // Hoặc giữ nguyên nếu bạn muốn hiển thị đường dẫn tạm thời
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Image Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _selectedImageBytes = null; // Xóa dữ liệu ảnh nếu có lỗi
                    _selectedImageFileName = null;
                }
            }
        }

        // CẦN THÊM phương thức này vào ClientService để tải ảnh về từ server
        // Nếu không có API tải ảnh, bạn sẽ cần một cách khác để hiển thị ảnh từ server (ví dụ: HTTP link trực tiếp đến server's image folder)
        // Đây chỉ là một ví dụ minh họa cách client có thể lấy ảnh từ server.
        // Thực tế, bạn cần một API trên server trả về byte[] của ảnh dựa trên tên file.
        // Tôi sẽ thêm phương thức này vào ClientService ở trên để hoàn chỉnh.
    }
}