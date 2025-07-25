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
    public partial class frmManufacturers : Form
    {
        bool signAdd = false;
        int id; // Keep ID for edit functionality

        BindingSource binding = new BindingSource();
        private readonly ClientService _clientService;

        public frmManufacturers(ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();

            string helpURL = ConfigurationManager.AppSettings["HelpURL"]?.ToString() ?? string.Empty;
            helpProvider1.HelpNamespace = helpURL + "manufacturers.html";
        }

        private void EnableControls(bool value)
        {
            btnSave.Enabled = value;
            btnCancel.Enabled = value;
            txtManufacturerName.Enabled = value;
            txtManufacturerAddress.Enabled = value;
            txtManufacturerPhone.Enabled = value;
            txtManufacturerEmail.Enabled = value;

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
                    await LoadManufacturers(); // Load lại toàn bộ danh sách từ server
                }
                else
                {
                    try
                    {
                        // Sửa kiểu trả về thành ServerResponse<List<ManufacturerDTO>>
                        ServerResponse<List<ManufacturerDTO>> response =
                            await _clientService.SendRequest<string, ServerResponse<List<ManufacturerDTO>>>("GetManufacturersByName", keyword);

                        if (response.Success && response.Data != null)
                        {
                            lblMessage.Text = ""; // Clear message if results found
                            binding.DataSource = response.Data;
                        }
                        else
                        {
                            lblMessage.Text = response.Message ?? "No matching manufacturer found.";
                            binding.DataSource = new List<ManufacturerDTO>(); // Clear DataGridView
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error searching manufacturers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblMessage.Text = "Error during search.";
                    }
                }
            };

            txtFind.TextChanged += (s, e) =>
            {
                lblMessage.Text = ""; // Clear the message label when text changes
            };
        }

        private async void Manufacturer_Load(object sender, EventArgs e)
        {
            EnableControls(false);
            await LoadManufacturers(); // Gọi hàm load riêng
            SetupToolStrip(); // Setup ToolStrip sau khi data đã load

            txtManufacturerName.DataBindings.Clear();
            txtManufacturerAddress.DataBindings.Clear();
            txtManufacturerPhone.DataBindings.Clear();
            txtManufacturerEmail.DataBindings.Clear();

            txtManufacturerName.DataBindings.Add("Text", binding, "ManufacturerName", false, DataSourceUpdateMode.Never);
            txtManufacturerAddress.DataBindings.Add("Text", binding, "ManufacturerAddress", false, DataSourceUpdateMode.Never);
            txtManufacturerPhone.DataBindings.Add("Text", binding, "ManufacturerPhone", false, DataSourceUpdateMode.Never);
            txtManufacturerEmail.DataBindings.Add("Text", binding, "ManufacturerEmail", false, DataSourceUpdateMode.Never);

            dataGridView.DataSource = binding;

            binding.CurrentChanged += (s, args) =>
            {
                if (binding.Current is ManufacturerDTO currentManufacturer)
                {
                    id = currentManufacturer.ID;
                }
            };
        }

        // Hàm riêng để load Manufacturers từ Server
        private async Task LoadManufacturers()
        {
            try
            {
                // Sửa kiểu trả về thành ServerResponse<List<ManufacturerDTO>>
                ServerResponse<List<ManufacturerDTO>> response = await _clientService.SendRequest<object, ServerResponse<List<ManufacturerDTO>>>("GetAllManufacturers", null);
                if (response.Success && response.Data != null)
                {
                    binding.DataSource = response.Data;
                }
                else
                {
                    MessageBox.Show(response.Message ?? "Failed to load manufacturers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    binding.DataSource = new List<ManufacturerDTO>(); // Xóa dữ liệu nếu có lỗi
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading manufacturers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                binding.DataSource = new List<ManufacturerDTO>(); // Xóa dữ liệu nếu có lỗi
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            signAdd = true;
            EnableControls(true);
            txtManufacturerName.Clear();
            txtManufacturerAddress.Clear();
            txtManufacturerPhone.Clear();
            txtManufacturerEmail.Clear();
            txtManufacturerName.Focus();
            binding.AddNew();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (binding.Current == null)
            {
                MessageBox.Show("Please select a manufacturer to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            signAdd = false;
            EnableControls(true);
            ManufacturerDTO currentManufacturer = binding.Current as ManufacturerDTO;
            if (currentManufacturer != null)
            {
                id = currentManufacturer.ID;
            }
            txtManufacturerName.Focus();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Client-side validation
                if (string.IsNullOrEmpty(txtManufacturerName.Text.Trim()) ||
                    string.IsNullOrEmpty(txtManufacturerAddress.Text.Trim()) ||
                    string.IsNullOrEmpty(txtManufacturerPhone.Text.Trim()) ||
                    string.IsNullOrEmpty(txtManufacturerEmail.Text.Trim()))
                {
                    MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dto = new ManufacturerDTO
                {
                    ManufacturerName = txtManufacturerName.Text.Trim(),
                    ManufacturerAddress = txtManufacturerAddress.Text.Trim(),
                    ManufacturerPhone = txtManufacturerPhone.Text.Trim(),
                    ManufacturerEmail = txtManufacturerEmail.Text.Trim()
                };

                // Thay đổi kiểu trả về của SendRequest sang ServerResponse<object>
                // (hoặc ServerResponse<ManufacturerDTO> nếu server trả về DTO đã thao tác)
                ServerResponse<object> response;

                if (signAdd)
                {
                    // TRequestPayload là ManufacturerDTO, TResponseData là ServerResponse<object>
                    response = await _clientService.SendRequest<ManufacturerDTO, ServerResponse<object>>("AddManufacturer", dto);
                }
                else
                {
                    dto.ID = id; // Gán lại ID cho DTO khi update
                    // TRequestPayload là ManufacturerDTO, TResponseData là ServerResponse<object>
                    response = await _clientService.SendRequest<ManufacturerDTO, ServerResponse<object>>("UpdateManufacturer", dto);
                }

                if (response.Success)
                {
                    MessageBox.Show("Operation successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadManufacturers(); // Tải lại dữ liệu sau khi lưu
                    EnableControls(false);
                }
                else
                {
                    MessageBox.Show(response.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving manufacturer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (binding.Current == null)
            {
                MessageBox.Show("Please select a manufacturer to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this manufacturer?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    ManufacturerDTO selectedManufacturer = binding.Current as ManufacturerDTO;
                    if (selectedManufacturer != null)
                    {
                        int idToDelete = selectedManufacturer.ID;

                        // TRequestPayload là int, TResponseData là ServerResponse<object>
                        ServerResponse<object> response = await _clientService.SendRequest<int, ServerResponse<object>>("DeleteManufacturer", idToDelete);

                        if (response.Success)
                        {
                            MessageBox.Show("Manufacturer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await LoadManufacturers(); // Tải lại dữ liệu sau khi xóa
                        }
                        else
                        {
                            MessageBox.Show(response.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting manufacturer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            EnableControls(false);
            await LoadManufacturers(); // Tải lại dữ liệu gốc
        }

        private async void btnClear_Click(object sender, EventArgs e)
        {
            txtFind.Clear();
            await LoadManufacturers(); // Tải lại toàn bộ danh sách
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
                    List<ManufacturerDTO> manufacturersToImport = new List<ManufacturerDTO>();
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
                                    columnIndexes[cell.Value.ToString()] = colIndex;
                                    colIndex++;
                                }
                                firstRow = false;
                                if (!columnIndexes.ContainsKey("ManufacturerName") ||
                                    !columnIndexes.ContainsKey("ManufacturerAddress") ||
                                    !columnIndexes.ContainsKey("ManufacturerPhone") ||
                                    !columnIndexes.ContainsKey("ManufacturerEmail"))
                                {
                                    MessageBox.Show("Excel file must contain 'ManufacturerName', 'ManufacturerAddress', 'ManufacturerPhone', 'ManufacturerEmail' columns.", "Import Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                continue;
                            }

                            try
                            {
                                var dto = new ManufacturerDTO
                                {
                                    ManufacturerName = row.Cell(columnIndexes["ManufacturerName"]).Value.ToString(),
                                    ManufacturerAddress = row.Cell(columnIndexes["ManufacturerAddress"]).Value.ToString(),
                                    ManufacturerPhone = row.Cell(columnIndexes["ManufacturerPhone"]).Value.ToString(),
                                    ManufacturerEmail = row.Cell(columnIndexes["ManufacturerEmail"]).Value.ToString()
                                };
                                manufacturersToImport.Add(dto);
                            }
                            catch (Exception rowEx)
                            {
                                MessageBox.Show($"Error reading data from a row: {rowEx.Message}. Skipping row.", "Import Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }

                        if (manufacturersToImport.Count == 0 && !firstRow)
                        {
                            MessageBox.Show("No valid manufacturer data found in the Excel file after headers.", "Import Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else if (firstRow)
                        {
                            MessageBox.Show("Excel file is empty or contains only headers.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        int successCount = 0;
                        int errorCount = 0;

                        foreach (var dto in manufacturersToImport)
                        {
                            try
                            {
                                // TRequestPayload là ManufacturerDTO, TResponseData là ServerResponse<object>
                                ServerResponse<object> response = await _clientService.SendRequest<ManufacturerDTO, ServerResponse<object>>("AddManufacturer", dto);
                                if (response.Success)
                                {
                                    successCount++;
                                }
                                else
                                {
                                    errorCount++;
                                    Console.WriteLine($"Error importing manufacturer '{dto.ManufacturerName}': {response.Message}");
                                }
                            }
                            catch (Exception ex)
                            {
                                errorCount++;
                                Console.WriteLine($"Error processing manufacturer '{dto.ManufacturerName}': {ex.Message}");
                            }
                        }

                        MessageBox.Show($"{successCount} manufacturers imported successfully. {errorCount} manufacturers failed to import.", "Import Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadManufacturers(); // Tải lại dữ liệu sau khi import
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
            saveFileDialog.FileName = "Manufacturers_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // TRequestPayload là object (null), TResponseData là List<ManufacturerDTO>
                    ServerResponse<List<ManufacturerDTO>> response = await _clientService.SendRequest<object, ServerResponse<List<ManufacturerDTO>>>("GetAllManufacturers", null);
                    List<ManufacturerDTO> manufacturersToExport = response.Data; // Lấy Data từ ServerResponse

                    DataTable table = new DataTable();
                    table.Columns.AddRange(new DataColumn[5]
                    {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("ManufacturerName", typeof(string)),
                        new DataColumn("ManufacturerAddress", typeof(string)),
                        new DataColumn("ManufacturerPhone", typeof(string)),
                        new DataColumn("ManufacturerEmail", typeof(string))
                    });

                    if (manufacturersToExport != null)
                    {
                        foreach (var m in manufacturersToExport)
                            table.Rows.Add(m.ID, m.ManufacturerName, m.ManufacturerAddress, m.ManufacturerPhone, m.ManufacturerEmail);
                    }
                    else
                    {
                        MessageBox.Show("No data to export.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "Manufacturers");
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