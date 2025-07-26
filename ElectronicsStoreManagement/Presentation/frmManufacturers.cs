using ClosedXML.Excel;
using ElectronicsStore.Client;
using ElectronicsStore.DataTransferObject; // Assuming you will have ManufacturerDTO here
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
        int id;

        BindingSource binding = new BindingSource();
        private readonly ClientService _clientService;

        public frmManufacturers(ClientService clientService)
        {
            _clientService = clientService;
            InitializeComponent();

            string helpURL = ConfigurationManager.AppSettings["HelpURL"]?.ToString() ?? string.Empty; // Handle null
            helpProvider1.HelpNamespace = helpURL + "manufacturers.html"; // Updated help URL
        }

        private void EnableControls(bool value)
        {
            btnSave.Enabled = value;
            btnCancel.Enabled = value;
            txtManufacturerName.Enabled = value; // Changed to txtManufacturerName

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
                    await LoadManufacturers(); // Load all manufacturers from server
                }
                else
                {
                    try
                    {
                        // ClientService.SendRequest already handles ServerResponse and returns Data directly
                        List<ManufacturerDTO> manufacturers = await _clientService.SendRequest<string, List<ManufacturerDTO>>("GetManufacturersByName", keyword); // Changed method name

                        if (manufacturers != null && manufacturers.Any())
                        {
                            lblMessage.Text = ""; // Clear message if results found
                            binding.DataSource = manufacturers;
                        }
                        else
                        {
                            lblMessage.Text = "No matching manufacturer found."; // Updated message
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

        private async void Manufacturers_Load(object sender, EventArgs e) // Changed method name
        {
            EnableControls(false);
            await LoadManufacturers(); // Call separate load manufacturer function
            SetupToolStrip(); // Setup ToolStrip after data has loaded

            // Clear old DataBindings before adding new ones
            txtManufacturerName.DataBindings.Clear(); // Changed to txtManufacturerName
            // Add new DataBindings
            txtManufacturerName.DataBindings.Add("Text", binding, "ManufacturerName", false, DataSourceUpdateMode.Never); // Changed to ManufacturerName
            dataGridView.DataSource = binding;

            // Ensure DataGridView updates display when binding source changes
            binding.CurrentChanged += (s, args) =>
            {
                // Logic to update UI when current position in binding changes (if needed)
            };
        }

        private async Task LoadManufacturers() // Changed method name
        {
            try
            {
                // ClientService.SendRequest already handles ServerResponse and returns Data directly
                List<ManufacturerDTO> manufacturers = await _clientService.SendRequest<object, List<ManufacturerDTO>>("GetAllManufacturers", null); // Changed method name
                if (manufacturers != null)
                {
                    binding.DataSource = manufacturers;
                }
                else
                {
                    MessageBox.Show("Failed to load manufacturers. No data returned.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Updated message
                    binding.DataSource = new List<ManufacturerDTO>(); // Clear data if error
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading manufacturers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Updated message
                binding.DataSource = new List<ManufacturerDTO>(); // Clear data if error
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            signAdd = true;
            EnableControls(true);
            txtManufacturerName.Clear(); // Changed to txtManufacturerName
            txtManufacturerName.Focus(); // Focus on input field
            binding.AddNew(); // Add a new row to BindingSource to allow new data entry
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (binding.Current == null) // Ensure a row is selected
            {
                MessageBox.Show("Please select a manufacturer to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); // Updated message
                return;
            }

            signAdd = false;
            EnableControls(true);
            // Get ID from current ManufacturerDTO object in BindingSource
            ManufacturerDTO currentManufacturer = binding.Current as ManufacturerDTO; // Changed to ManufacturerDTO
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
                var dto = new ManufacturerDTO { ManufacturerName = txtManufacturerName.Text.Trim() }; // Changed to ManufacturerDTO and ManufacturerName

                // Since SendRequest returns TResponseData (Data extracted from ServerResponse),
                // here we just need to check if SendRequest was successful
                // by catching exceptions (if there's a server error, SendRequest will throw an exception).
                // If no exception, it means success.
                object result = null; // Use object because server might return null/void for add/edit/delete operations

                if (string.IsNullOrEmpty(dto.ManufacturerName)) // Changed to ManufacturerName
                {
                    MessageBox.Show("Manufacturer Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning); // Updated message
                    return;
                }

                if (signAdd)
                {
                    result = await _clientService.SendRequest<ManufacturerDTO, object>("AddManufacturer", dto); // Changed method name
                }
                else
                {
                    dto.ID = id; // Assign ID back to DTO when updating
                    result = await _clientService.SendRequest<ManufacturerDTO, object>("UpdateManufacturer", dto); // Changed method name
                }

                // If no exception is thrown, it means success
                MessageBox.Show("Operation successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadManufacturers(); // Reload data after saving
                EnableControls(false);
            }
            catch (Exception ex)
            {
                // Any error from the server (Success = false) or network error has been encapsulated and thrown by ClientService
                MessageBox.Show($"Error saving manufacturer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Updated message
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (binding.Current == null)
            {
                MessageBox.Show("Please select a manufacturer to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); // Updated message
                return;
            }

            if (MessageBox.Show("Are you sure you want to delete this manufacturer?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) // Updated message
            {
                try
                {
                    ManufacturerDTO selectedManufacturer = binding.Current as ManufacturerDTO; // Changed to ManufacturerDTO
                    if (selectedManufacturer != null)
                    {
                        int idToDelete = selectedManufacturer.ID;

                        // SendRequest will throw an exception if server reports an error or there's a network issue
                        await _clientService.SendRequest<int, object>("DeleteManufacturer", idToDelete); // Changed method name

                        // If no exception, it means success
                        MessageBox.Show("Manufacturer deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); // Updated message
                        await LoadManufacturers(); // Reload data after deleting
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting manufacturer: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // Updated message
                }
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            EnableControls(false);
            await LoadManufacturers(); // Reload original data
        }

        private async void btnClear_Click(object sender, EventArgs e)
        {
            txtFind.Clear();
            await LoadManufacturers(); // Reload entire list
        }

        // --- Import/Export functions ---
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
                                var dto = new ManufacturerDTO // Changed to ManufacturerDTO
                                {
                                    ManufacturerName = r["ManufacturerName"].ToString() // Ensure column name matches Excel
                                };

                                // SendRequest will throw an exception if there's an error, otherwise it's considered successful
                                await _clientService.SendRequest<ManufacturerDTO, object>("AddManufacturer", dto); // Changed method name
                                successCount++;
                            }
                            catch (Exception ex)
                            {
                                errorCount++;
                                Console.WriteLine($"Error importing row '{r["ManufacturerName"]}': {ex.Message}"); // Changed to ManufacturerName
                            }
                        }

                        MessageBox.Show($"{successCount} manufacturers imported successfully. {errorCount} manufacturers failed to import.", "Import Result", MessageBoxButtons.OK, MessageBoxIcon.Information); // Updated message
                        await LoadManufacturers(); // Reload data after import
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
            saveFileDialog.FileName = "Manufacturers_" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".xlsx"; // Updated file name
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Get all manufacturers to export
                    List<ManufacturerDTO> manufacturersToExport = await _clientService.SendRequest<object, List<ManufacturerDTO>>("GetAllManufacturers", null); // Changed method name

                    if (manufacturersToExport == null || !manufacturersToExport.Any())
                    {
                        MessageBox.Show("No manufacturers to export.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); // Updated message
                        return;
                    }

                    // Create DataTable from List<ManufacturerDTO>
                    DataTable table = new DataTable();
                    table.Columns.AddRange(new DataColumn[2]
                    {
                        new DataColumn("ID", typeof(int)),
                        new DataColumn("ManufacturerName", typeof(string)) // Changed to ManufacturerName
                    });

                    foreach (var p in manufacturersToExport)
                        table.Rows.Add(p.ID, p.ManufacturerName); // Changed to ManufacturerName

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var sheet = wb.Worksheets.Add(table, "Manufacturers"); // Changed sheet name
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