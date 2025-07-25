using ClosedXML.Excel;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectronicsStore.Presentation
{
    public partial class frmCategories : Form
    {
        bool signAdd = false;
        int id;

        BindingSource binding = new BindingSource();
        private readonly ClientService _clientService;

        public frmCategories(ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();

            string helpURL = ConfigurationManager.AppSettings["HelpURL"]?.ToString() ?? string.Empty; // Xử lý null
            helpProvider1.HelpNamespace = helpURL + "categories.html";
        }

        private void EnableControls(bool value)
        {
            btnSave.Enabled = value;
            btnCancel.Enabled = value;
            txtCategoryName.Enabled = value;

            btnAdd.Enabled = !value;
            btnEdit.Enabled = !value;
            btnDelete.Enabled = !value;
            btnImport.Enabled = !value;
            btnExport.Enabled = !value;
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
                    await LoadCategories(); // Load lại toàn bộ danh sách từ server
                }
                else
                {
                    try
                    {
                        // Sửa từ ClientService.SendRequest<string, List<CategoryDTO>>
                        // thành ClientService.SendRequest<string, ServerResponse<List<CategoryDTO>>>
                        ServerResponse<List<CategoryDTO>> response = await _clientService.SendRequest<string, ServerResponse<List<CategoryDTO>>>("GetCategoriesByName", keyword);

                        if (response.Success && response.Data != null)
                        {
                            lblMessage.Text = ""; // Clear message if results found
                            binding.DataSource = response.Data;
                        }
                        else
                        {
                            lblMessage.Text = response.Message ?? "No matching category found.";
                            binding.DataSource = new List<CategoryDTO>(); // Clear DataGridView
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error searching categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblMessage.Text = "Error during search.";
                    }
                }
            };

            txtFind.TextChanged += (s, e) =>
            {
                lblMessage.Text = ""; // Clear the message label when text changes
            };
        }

        private async void Categories_Load(object sender, EventArgs e)
        {
            EnableControls(false);
            await LoadCategories(); // Gọi hàm load category riêng
            SetupToolStrip(); // Setup ToolStrip sau khi data đã load

            // Clear DataBindings cũ trước khi thêm mới
            txtCategoryName.DataBindings.Clear();
            // Thêm DataBindings mới
            txtCategoryName.DataBindings.Add("Text", binding, "CategoryName", false, DataSourceUpdateMode.Never);
            dataGridView.DataSource = binding;

            // Đảm bảo DataGridView cập nhật hiển thị khi binding source thay đổi
            binding.CurrentChanged += (s, args) =>
            {
                // Logic cập nhật UI khi vị trí hiện tại trong binding thay đổi (nếu cần)
            };
        }

        private async Task LoadCategories()
        {
            try
            {
                // Sửa từ ClientService.SendRequest<object, List<CategoryDTO>>
                // thành ClientService.SendRequest<object, ServerResponse<List<CategoryDTO>>>
                ServerResponse<List<CategoryDTO>> response = await _clientService.SendRequest<object, ServerResponse<List<CategoryDTO>>>("GetAllCategories", null);
                if (response.Success && response.Data != null)
                {
                    binding.DataSource = response.Data;
                }
                else
                {
                    MessageBox.Show(response.Message ?? "Failed to load categories.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    binding.DataSource = new List<CategoryDTO>(); // Xóa dữ liệu nếu có lỗi
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                binding.DataSource = new List<CategoryDTO>(); // Xóa dữ liệu nếu có lỗi
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            signAdd = true;
            EnableControls(true);
            txtCategoryName.Clear();
            txtCategoryName.Focus(); // Focus vào ô nhập liệu
            binding.AddNew(); // Thêm một dòng mới vào BindingSource để có thể nhập dữ liệu mới
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (binding.Current == null) // Đảm bảo có dòng được chọn
            {
                MessageBox.Show("Please select a category to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            signAdd = false;
            EnableControls(true);
            // Lấy ID từ đối tượng CategoryDTO hiện tại trong BindingSource
            CategoryDTO currentCategory = binding.Current as CategoryDTO;
            if (currentCategory != null)
            {
                id = currentCategory.ID;
            }
            txtCategoryName.Focus();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = new CategoryDTO { CategoryName = txtCategoryName.Text.Trim() };
                // Thay đổi kiểu trả về của SendRequest sang ServerResponse<CategoryDTO> hoặc ServerResponse<object>
                // Tùy thuộc vào việc server có trả về DTO đã thao tác hay không.
                // Nếu chỉ cần kiểm tra Success/Message, có thể dùng ServerResponse<object>
                // Nếu server trả về CategoryDTO đã được thêm/cập nhật, thì dùng ServerResponse<CategoryDTO>
                ServerResponse<object> response; // Dùng object vì bạn chỉ cần Success/Message ở đây

                if (string.IsNullOrEmpty(dto.CategoryName))
                {
                    MessageBox.Show("Category Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (signAdd)
                {
                    response = await _clientService.SendRequest<CategoryDTO, ServerResponse<object>>("AddCategory", dto);
                }
                else
                {
                    dto.ID = id; // Gán lại ID cho DTO khi update
                    response = await _clientService.SendRequest<CategoryDTO, ServerResponse<object>>("UpdateCategory", dto);
                }

                if (response.Success)
                {
                    MessageBox.Show("Operation successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadCategories(); // Tải lại dữ liệu sau khi lưu
                    EnableControls(false);
                }
                else
                {
                    MessageBox.Show(response.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (binding.Current == null)
            {
                MessageBox.Show("Please select a category to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this category?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    CategoryDTO selectedCategory = binding.Current as CategoryDTO;
                    if (selectedCategory != null)
                    {
                        int idToDelete = selectedCategory.ID;

                        // Sửa từ ClientService.SendRequest<int, ServerResponse>
                        // thành ClientService.SendRequest<int, ServerResponse<object>>
                        ServerResponse<object> response = await _clientService.SendRequest<int, ServerResponse<object>>("DeleteCategory", idToDelete);

                        if (response.Success)
                        {
                            MessageBox.Show("Category deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadCategories(); // Tải lại dữ liệu sau khi xóa
                        }
                        else
                        {
                            MessageBox.Show(response.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            EnableControls(false);
            await LoadCategories(); // Tải lại dữ liệu gốc
        }

        private async void btnClear_Click(object sender, EventArgs e)
        {
            txtFind.Clear();
            await LoadCategories(); // Tải lại toàn bộ danh sách
        }

        // --- Các hàm Import/Export ---
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
                            MessageBox.Show("Excel file is empty or contains only headers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        int successCount = 0;
                        int errorCount = 0;
                        foreach (DataRow r in table.Rows)
                        {
                            try
                            {
                                var dto = new CategoryDTO
                                {
                                    CategoryName = r["CategoryName"].ToString() // Đảm bảo tên cột khớp với Excel
                                };

                                // Sửa từ ClientService.SendRequest<CategoryDTO, ServerResponse>
                                // thành ClientService.SendRequest<CategoryDTO, ServerResponse<object>>
                                ServerResponse<object> response = await _clientService.SendRequest<CategoryDTO, ServerResponse<object>>("AddCategory", dto);
                                if (response.Success)
                                {
                                    successCount++;
                                }
                                else
                                {
                                    errorCount++;
                                    Console.WriteLine($"Error importing row '{dto.CategoryName}': {response.Message}");
                                }
                            }
                            catch (Exception ex)
                            {
                                errorCount++;
                                Console.WriteLine($"Error importing row: {ex.Message}");
                            }
                        }

                        MessageBox.Show($"{successCount} categories imported successfully. {errorCount} categories failed to import.", "Import Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadCategories(); // Tải lại dữ liệu sau khi import
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during import: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private async void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export to Excel file";
            saveFileDialog.Filter = "Excel file|*.xls;*.xlsx";
            saveFileDialog.FileName = "Categories_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Sửa từ ClientService.SendRequest<object, List<CategoryDTO>>
                    // thành ClientService.SendRequest<object, ServerResponse<List<CategoryDTO>>>
                    ServerResponse<List<CategoryDTO>> response = await _clientService.SendRequest<object, ServerResponse<List<CategoryDTO>>>("GetAllCategories", null);
                    List<CategoryDTO> categoriesToExport = new List<CategoryDTO>();

                    if (response.Success && response.Data != null)
                    {
                        categoriesToExport = response.Data;
                    }
                    else
                    {
                        MessageBox.Show(response.Message ?? "Failed to retrieve categories for export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Không xuất file nếu không lấy được dữ liệu
                    }

                    // Tạo DataTable từ List<CategoryDTO>
                    DataTable table = new DataTable();
                    table.Columns.AddRange(new DataColumn[2]
                    {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("CategoryName", typeof(string))
                    });

                    if (categoriesToExport != null)
                    {
                        foreach (var p in categoriesToExport)
                            table.Rows.Add(p.ID, p.CategoryName);
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "Categories");
                        sheet.Columns().AdjustToContents();
                        wb.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Exported data to Excel file successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during export: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}