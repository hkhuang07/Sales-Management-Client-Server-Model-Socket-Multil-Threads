using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElectronicsStore.DataTransferObject; // Make sure this namespace is included
using ElectronicsStore.Client;
using Newtonsoft.Json;

namespace ElectronicsStore.Presentation
{
    public partial class frmMain : Form
    {
        // Khai báo các form con
        frmLogin logIn = null;
        frmChangePass changePass = null;
        frmSale sale = null;
        frmCategories categories = null;
        frmManufacturers manufacturers = null;
        frmProducts products = null;
        frmCustomers customers = null;
        frmEmployees employees = null;
        frmOrders orders = null;
        // frmOrderDetails orderDetails = null; // This form is usually for details of a single order, often opened from frmOrders, so it might not need to be a direct MDI child of frmMain unless your design dictates it.
        frmProductStatistics productStatistics = null;
        frmRevenueStatistics revenueStatistics = null;
        AboutBox about = null;
        Flash flash = null;

        // Sử dụng ClientService để giao tiếp với Server
        private readonly ClientService _clientService;

        public string employeeName = ""; // Tên người dùng hiển thị vào thanh Status.
        public int currentEmployeeId = -1; // ID của nhân viên đang đăng nhập
        public bool currentEmployeeRole = false; // Vai trò của nhân viên đang đăng nhập (true = Admin, false = Staff)

        public frmMain()
        {
            // The splash screen is typically shown before InitializeComponent() if it's part of the main application startup
            flash = new Flash();
            flash.ShowDialog();

            // Khởi tạo ClientService ngay trong constructor của frmMain
            string serverIp = ConfigurationManager.AppSettings["ServerIp"] ?? "127.0.0.1"; // Default to localhost if not found
            int port = int.Parse(ConfigurationManager.AppSettings["ServerPort"] ?? "301"); // Default port

            _clientService = new ClientService(serverIp, port);
            InitializeComponent();

            string helpURL = ConfigurationManager.AppSettings["HelpURL"]?.ToString();
            helpProvider1.HelpNamespace = helpURL + "main.html";
        }

        private async void frmMain_Load(object sender, EventArgs e)
        {
            NotLoggedIn();
            await LogIn(); // Chờ LogIn hoàn thành
        }


        // Event Handlers

        private async void mnuLogIn_Click(object sender, EventArgs e)
        {
            await LogIn();
        }

        private void mnuLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to log out ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Đóng tất cả các form con đang mở
                foreach (Form child in MdiChildren)
                {
                    child.Close();
                }
                NotLoggedIn();
                // Reset thông tin nhân viên khi đăng xuất
                employeeName = "";
                currentEmployeeId = -1;
                currentEmployeeRole = false;
            }
        }

        private void mnuChangePass_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(employeeName) || currentEmployeeId == -1)
            {
                MessageBox.Show("You are not logged in!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (changePass == null || changePass.IsDisposed)
            {
                // Pass currentEmployeeId, employeeName, and _clientService
                changePass = new frmChangePass(currentEmployeeId, employeeName, _clientService);
                changePass.MdiParent = this;
                changePass.Show();
            }
            else
            {
                changePass.Activate();
            }
        }

        private async void mnuRestore_Click(object sender, EventArgs e)
        {
            OpenFileDialog restoreDialog = new OpenFileDialog();
            restoreDialog.Filter = "Backup files (*.bak)|*.bak";
            restoreDialog.Title = "Select the backup file (.bak) to restore";

            if (restoreDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = restoreDialog.FileName;

                if (!string.IsNullOrEmpty(filePath))
                {
                    try
                    {
                        // Thay đổi kiểu trả về từ ServerResponse<bool> thành bool
                        bool success = await _clientService.SendRequest<string, bool>("RestoreDatabase", filePath);

                        if (success) // Chỉ cần kiểm tra giá trị boolean trực tiếp
                        {
                            MessageBox.Show("Data recovery successful!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Logic này có thể không được chạy nếu ClientService ném Exception khi operation thất bại.
                            // Tuy nhiên, nếu server trả về false mà không kèm theo Exception, thì nó sẽ được bắt ở đây.
                            MessageBox.Show("Data recovery failed! Please check server logs for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Mọi lỗi từ ClientService (bao gồm cả lỗi từ serverResponse.Message nếu Success là false)
                        // đều sẽ được bắt ở đây vì ClientService đang ném Exception.
                        MessageBox.Show($"An error occurred during restore: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
     
        private async void mnuBackup_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog backupFolder = new FolderBrowserDialog();
            backupFolder.Description = "Select folder to backup data";

            if (backupFolder.ShowDialog() == DialogResult.OK)
            {
                string path = backupFolder.SelectedPath;

                if (!string.IsNullOrEmpty(path))
                {
                    try
                    {
                        // Thay đổi kiểu trả về từ ServerResponse<bool> thành bool
                        bool success = await _clientService.SendRequest<string, bool>("BackupDatabase", path);

                        if (success) // Chỉ cần kiểm tra giá trị boolean trực tiếp
                        {
                            MessageBox.Show("Data backup successful!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            // Logic này có thể không được chạy nếu ClientService ném Exception khi operation thất bại.
                            // Tuy nhiên, nếu server trả về false mà không kèm theo Exception, thì nó sẽ được bắt ở đây.
                            MessageBox.Show("Data backup failed! Please check server logs for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Mọi lỗi từ ClientService (bao gồm cả lỗi từ serverResponse.Message nếu Success là false)
                        // đều sẽ được bắt ở đây vì ClientService đang ném Exception.
                        MessageBox.Show($"An error occurred during backup: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
   

        private void mnuExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to exit the program?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Open child forms, passing the _clientService instance
        private void mnuSale_Click(object sender, EventArgs e)
        {
            
            if (sale == null || sale.IsDisposed)
            {
                sale = new frmSale(_clientService);
                sale.MdiParent = this;
                sale.Show();
            }
            else
                sale.Activate();
        }

        private void mnuCategories_Click(object sender, EventArgs e)
        {
            if (categories == null || categories.IsDisposed)
            {
                categories = new frmCategories(_clientService);
                categories.MdiParent = this;
                categories.Show();
            }
            else
                categories.Activate();
        }

        private void mnuManufacturers_Click(object sender, EventArgs e)
        {
            if (manufacturers == null || manufacturers.IsDisposed)
            {
                manufacturers = new frmManufacturers(_clientService);
                manufacturers.MdiParent = this;
                manufacturers.Show();
            }
            else
                manufacturers.Activate();
        }

        private void mnuProducts_Click(object sender, EventArgs e)
        {
            if (products == null || products.IsDisposed)
            {
                products = new frmProducts(_clientService);
                products.MdiParent = this;
                products.Show();
            }
            else
                products.Activate();
        }

        private void mnuOrders_Click(object sender, EventArgs e)
        {
            if (orders == null || orders.IsDisposed)
            {
                orders = new frmOrders(_clientService);
                orders.MdiParent = this;
                orders.Show();
            }
            else
                orders.Activate();
        }

        private void mnuCustomers_Click(object sender, EventArgs e)
        {
            if (customers == null || customers.IsDisposed)
            {
                customers = new frmCustomers(_clientService);
                customers.MdiParent = this;
                customers.Show();
            }
            else
                customers.Activate();
        }

        private void mnuEmployees_Click(object sender, EventArgs e)
        {
            if (employees == null || employees.IsDisposed)
            {
                // Only allow Admin to access Employee Management
                if (currentEmployeeRole == true)
                {
                    employees = new frmEmployees(_clientService);
                    employees.MdiParent = this;
                    employees.Show();
                }
                else
                {
                    MessageBox.Show("You do not have permission to access Employee Management.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (currentEmployeeRole == true)
                {
                    employees.Activate();
                }
                else
                {
                    MessageBox.Show("You do not have permission to access Employee Management.", "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void mnuProductStatistics_Click(object sender, EventArgs e)
        {
            if (productStatistics == null || productStatistics.IsDisposed)
            {
                productStatistics = new frmProductStatistics(_clientService);
                productStatistics.MdiParent = this;
                productStatistics.Show();
            }
            else
                productStatistics.Activate();
        }

        private void mnuRevenueStatistics_Click(object sender, EventArgs e)
        {
            if (revenueStatistics == null || revenueStatistics.IsDisposed)
            {
                revenueStatistics = new frmRevenueStatistics(_clientService);
                revenueStatistics.MdiParent = this;
                revenueStatistics.Show();
            }
            else
                revenueStatistics.Activate();
        }

        private void mnuSoftwareInformation_Click(object sender, EventArgs e)
        {
            about = new AboutBox();
            about.ShowDialog();
        }

        private void mnuUserGuide_Click(object sender, EventArgs e)
        {
            string helpurl = ConfigurationManager.AppSettings["HelpURL"]?.ToString();
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "explorer.exe";
            info.Arguments = helpurl + "index.html";
            Process.Start(info);
        }

        private void lblLink_Click(object sender, EventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "explorer.exe";
            info.Arguments = "[https://github.com/hkhuang07](https://github.com/hkhuang07)";
            Process.Start(info);
        }


        // System Process and UI State Management

        private async Task LogIn()
        {
            bool isAuthenticated = false;

            while (!isAuthenticated)
            {
                // Khởi tạo frmLogin và truyền _clientService vào
                logIn = new frmLogin(_clientService);

                // Hiển thị form đăng nhập
                if (logIn.ShowDialog() == DialogResult.OK)
                {
                    // Lấy thông tin đã đăng nhập từ Tag của frmLogin
                    var loginResponse = logIn.Tag as LoginResponseDTO;
                    if (loginResponse != null)
                    {
                        // Use the correct property names from LoginResponseDTO
                        // Assuming LoginResponseDTO has properties: FullName, EmployeeId, IsAdmin
                        employeeName = loginResponse.FullName;
                        currentEmployeeId = loginResponse.UserId;
                        currentEmployeeRole = loginResponse.Roles; // Assuming IsAdmin is the boolean property for role

                        isAuthenticated = true;

                        MessageBox.Show($"Welcome {employeeName}!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (logIn.chkSave.Checked == false)
                        {
                            logIn.txtUserName.Clear();
                            logIn.txtPassword.Clear();
                        }

                        if (currentEmployeeRole == true) // true = Admin
                            Administrator();
                        else // false = Staff
                            Member();
                    }
                    else
                    {
                        MessageBox.Show("Login failed unexpectedly. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        // Loop lại để người dùng nhập lại
                    }
                }
                else
                {
                    // Người dùng bấm Cancel hoặc đóng form đăng nhập
                    logIn.Dispose();
                    Application.Exit(); // Thoát ứng dụng nếu không đăng nhập
                    return;
                }
            }
        }

        public void NotLoggedIn()
        {
            // Enable login
            mnuLogIn.Enabled = true;
            btnLogin.Enabled = true;

            // Disable all other menu items and buttons
            mnuLogout.Enabled = false;
            mnuChangePass.Enabled = false;
            mnuData.Enabled = false;
            mnuSale.Enabled = false;
            managementToolStripMenuItem.Enabled = false;
            reportStatisticToolStripMenuItem.Enabled = false;
            mnuCategories.Enabled = false;
            mnuManufacturers.Enabled = false;
            mnuProducts.Enabled = false;
            mnuCustomers.Enabled = false;
            mnuEmployees.Enabled = false;
            mnuOrders.Enabled = false;
            mnuProductStatistics.Enabled = false;
            mnuRevenueStatistics.Enabled = false;

            tabLogin.Enabled = true;
            tabLogout.Enabled = false;
            tabChangepass.Enabled = false;
            tabRestore.Enabled = false;
            tabBackup.Enabled = false;
            tabSale.Enabled = false;
            tabManagement.Enabled = false;
            tabReportStatistics.Enabled = false;
            tabCategories.Enabled = false;
            tabManufacturer.Enabled = false;
            tabProducts.Enabled = false;
            tabCustomers.Enabled = false;
            tabEmployees.Enabled = false;
            tabOrders.Enabled = false;
            tabProductStatistics.Enabled = false;
            tabRevenueStatistics.Enabled = false;

            btnLogout.Enabled = false;
            btnChangePass.Enabled = false;
            btnRestore.Enabled = false;
            btnBackup.Enabled = false;
            btnSale.Enabled = false;
            btnSales.Enabled = false;
            btnCategories.Enabled = false;
            btnManufacturers.Enabled = false;
            btnProducts.Enabled = false;
            btnCustomers.Enabled = false;
            btnEmployees.Enabled = false;
            btnOrders.Enabled = false;
            btnProductStatistics.Enabled = false;
            btnRevenueStatistics.Enabled = false;

            // Display status
            lblStatus.Text = "Not Logged In.";
        }

        private void Administrator()
        {
            // Disable login
            mnuLogIn.Enabled = false;
            btnLogin.Enabled = false;

            // Enable all admin-specific menu items and buttons
            mnuLogout.Enabled = true;
            mnuChangePass.Enabled = true;
            mnuData.Enabled = true;
            mnuSale.Enabled = true;
            managementToolStripMenuItem.Enabled = true;
            reportStatisticToolStripMenuItem.Enabled = true;
            mnuCategories.Enabled = true;
            mnuManufacturers.Enabled = true;
            mnuProducts.Enabled = true;
            mnuCustomers.Enabled = true;
            mnuEmployees.Enabled = true; // Admin can manage employees
            mnuOrders.Enabled = true;
            mnuProductStatistics.Enabled = true;
            mnuRevenueStatistics.Enabled = true;

            tabLogin.Enabled = false;
            tabLogout.Enabled = true;
            tabChangepass.Enabled = true;
            tabRestore.Enabled = true;
            tabBackup.Enabled = true;
            tabSale.Enabled = true;
            tabManagement.Enabled = true;
            tabReportStatistics.Enabled = true;
            tabCategories.Enabled = true;
            tabManufacturer.Enabled = true;
            tabProducts.Enabled = true;
            tabCustomers.Enabled = true;
            tabEmployees.Enabled = true;
            tabOrders.Enabled = true;
            tabProductStatistics.Enabled = true;
            tabRevenueStatistics.Enabled = true;

            btnLogout.Enabled = true;
            btnChangePass.Enabled = true;
            btnRestore.Enabled = true;
            btnBackup.Enabled = true;
            btnSale.Enabled = true;
            btnSales.Enabled = true;
            btnCategories.Enabled = true;
            btnManufacturers.Enabled = true;
            btnProducts.Enabled = true;
            btnCustomers.Enabled = true;
            btnEmployees.Enabled = true;
            btnOrders.Enabled = true;
            btnProductStatistics.Enabled = true;
            btnRevenueStatistics.Enabled = true;

            //lblLink.Visible = false; // You might want to control this based on your design
            lblStatus.Text = $"Welcome Administrator: {employeeName} to the program!";
        }

        private void Member()
        {
            // Disable login
            mnuLogIn.Enabled = false;
            btnLogin.Enabled = false;

            // Enable staff-specific menu items and buttons, disable admin-only ones
            mnuLogout.Enabled = true;
            mnuChangePass.Enabled = true;
            mnuData.Enabled = false; // Staff typically doesn't manage direct data (backup/restore)
            mnuSale.Enabled = true;
            managementToolStripMenuItem.Enabled = true;
            reportStatisticToolStripMenuItem.Enabled = false; // Staff might not see full statistics

            mnuCategories.Enabled = false;
            mnuManufacturers.Enabled = false;
            mnuProducts.Enabled = true;
            mnuCustomers.Enabled = true;
            mnuEmployees.Enabled = false; // Staff cannot manage employees
            mnuOrders.Enabled = true;
            mnuProductStatistics.Enabled = false;
            mnuRevenueStatistics.Enabled = false;


            tabLogin.Enabled = false;
            tabLogout.Enabled = true;
            tabChangepass.Enabled = true;
            tabRestore.Enabled = false; // Staff typically doesn't do backup/restore
            tabBackup.Enabled = false;
            tabSale.Enabled = true;
            tabManagement.Enabled = true;
            tabCategories.Enabled = false;
            tabManufacturer.Enabled = false;
            tabProducts.Enabled = true;
            tabCustomers.Enabled = true;
            tabEmployees.Enabled = false;
            tabOrders.Enabled = true;
            tabProductStatistics.Enabled = false;
            tabRevenueStatistics.Enabled = false;
            tabReportStatistics.Enabled = false;

            btnLogout.Enabled = true;
            btnChangePass.Enabled = true;
            btnRestore.Enabled = false;
            btnBackup.Enabled = false;
            btnSale.Enabled = true;
            btnSales.Enabled = true;
            btnCategories.Enabled = false;
            btnManufacturers.Enabled = false;
            btnProducts.Enabled = true;
            btnCustomers.Enabled = true;
            btnEmployees.Enabled = false;
            btnOrders.Enabled = true;
        }
    }
}