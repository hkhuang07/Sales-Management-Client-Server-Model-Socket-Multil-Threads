
using ElectronicsStore.BussinessLogic;
using ElectronicsStore.DataTransferObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Server
{
    public partial class frmServer : Form
    {
        private readonly CategoryService _categoryservice;
        BindingSource binding = new BindingSource();

        public frmServer()
        {
            InitializeComponent();
        }

        Socket server = null;
        NetworkStream networkStream;
        StreamReader streamReader;
        StreamWriter streamWriter;
        SqlConnection connection;

        string serverString = "";
        int choice = 0;

        public void OpenConnection()
        {
            serverString = @"Data Source=.;Database=ElectronsStore;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True";
            connection = new SqlConnection(serverString);
            connection.Open();
        }

        public static void CloseConnection(SqlConnection connection)
        {
            if (connection != null && connection.State == ConnectionState.Open)
                connection.Close();
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            btnStartServer.Text = "Server running...";
            Application.DoEvents();
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 5656);

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(ipEnd);
            server.Listen(10);
            Socket clientSock = server.Accept();

            OpenConnection();
            //DataTable table = getData();
            //  GuiDataTable(clientSock, table);


            networkStream = new NetworkStream(clientSock);
            streamReader = new StreamReader(networkStream);
            streamWriter = new StreamWriter(networkStream);

            while (true)
            {
                int choice = int.Parse(streamReader.ReadLine());
                if (choice == 1)
                {
                    string categoryID = streamReader.ReadLine();
                    string categoryName = streamReader.ReadLine();
                    //DataTable result = AddCategory(categoryID, categoryName);
                    //GuiDataTable(clientSock, result);
                }
                else if (choice == 2)
                {
                    string categoryid = streamReader.ReadLine();
                    //DataTable result = DeleteCategories(categoryID);
                    //GuiDataTable(clientSock, result);
                }
                else if (choice == 3)
                {
                    string categoryID = streamReader.ReadLine();
                    string categoryName = streamReader.ReadLine();
                    //DataTable result = UpdateCategory(categoryID, categoryName);
                    //GuiDataTable(clientSock, result);
                }
                else if (choice == 4)
                {
                    string key = streamReader.ReadLine();
                    //DataTable result = FindCategories(key);
                    //GuiDataTable(clientSock, result);
                }
            }

            CloseConnection(connection);
            clientSock.Close();
            server.Close();
            btnStartServer.Text = "Start Server";
            Application.DoEvents();
        }

        private void GuiDataTable(Socket clientSock, DataTable dt)
        {
            byte[] serialized = SerializeData(dt);
            byte[] lengthBytes = BitConverter.GetBytes(serialized.Length);
            clientSock.Send(lengthBytes);
            clientSock.Send(serialized);
        }

        public byte[] SerializeData(Object o)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(DataTable));
                serializer.WriteObject(ms, o);
                return ms.ToArray();
            }
        }

        private DataGridView getdata()
        {
            //EnableControls(false);
            var list = _categoryservice.GetAll();
            binding.DataSource = list;
            /*SetupToolStrip();
            txtCategoryName.DataBindings.Clear();
            txtCategoryName.DataBindings.Add("Text", binding, "CategoryName", false, DataSourceUpdateMode.Never);
            */
            DataGridView dgv = new DataGridView();
            dgv.DataSource = list;

            return dgv;
        }


        private void frmServer_Load(object sender, EventArgs e)
        {

        }
    }
}
