using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace ElectronicsStore.DataAccess
{
    public class DatabaseRepository
    {
        private readonly string _connectionString;

        public DatabaseRepository()
        {
            _connectionString = "Data Source=.;Database=ElectronsStore;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True";
            // Hoặc lấy từ appsettings.json hay App.config nếu có
        }

        public bool BackupDatabase(string backupFolderPath)
        {
            string databaseName = new SqlConnectionStringBuilder(_connectionString).InitialCatalog;
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string backupFilePath = System.IO.Path.Combine(backupFolderPath, $"{databaseName}_{timestamp}.bak");

            string sql = $@"
            BACKUP DATABASE [{databaseName}] 
            TO DISK = @backupFile 
            WITH FORMAT, INIT, NAME = 'Full Backup of {databaseName}'";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@backupFile", backupFilePath);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }

        private string GetLogicalFileName(string databaseName)
        {
            string logicalName = "";
            string sql = $"RESTORE FILELISTONLY FROM DISK = @backupFile";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@backupFile", @"D:\Study\Information_Manager_System_Programming\Project\Electronics Store Management Project\ElectronicsStoreManagement\Data");
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        logicalName = reader["LogicalName"].ToString();
                    }
                }
            }

            return logicalName;
        }


        public bool RestoreDatabase(string backupFilePath)
        {
            /*string databaseName = new SqlConnectionStringBuilder(_connectionString).InitialCatalog;

            string sql = $@"
            ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            RESTORE DATABASE [{databaseName}] FROM DISK = @backupFile WITH REPLACE;
            ALTER DATABASE [{databaseName}] SET MULTI_USER;";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@backupFile", backupFilePath);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }    */
            string databaseName = new SqlConnectionStringBuilder(_connectionString).InitialCatalog;
            string logicalName = GetLogicalFileName(databaseName);

            if (string.IsNullOrEmpty(logicalName))
                throw new Exception("Không thể lấy Logical File Name của database.");

            string sql = $@"
                            USE master;
                            ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;

                            RESTORE DATABASE [{databaseName}] 
                            FROM DISK = @backupFile 
                            WITH REPLACE,
                                 MOVE '{logicalName}' TO 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\{databaseName}.mdf',
                                 MOVE '{logicalName}_log' TO 'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\{databaseName}_log.ldf';

                            ALTER DATABASE [{databaseName}] SET MULTI_USER;
                            ";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@backupFile", backupFilePath);
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }

        }
    }
}
