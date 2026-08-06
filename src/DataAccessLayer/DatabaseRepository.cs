using System;
using System.Data;
using System.IO;
using Microsoft.Data.SqlClient;

namespace ElectronicsStore.DataAccess
{
    public class DatabaseRepository
    {
        private readonly string _connectionString;

        public DatabaseRepository()
        {
            _connectionString = "Data Source=.\\SQLEXPRESS;Database=ElectronicsStoreDB;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True";
        }

        public DatabaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool BackupDatabase(string backupFolderPath)
        {
            if (string.IsNullOrWhiteSpace(backupFolderPath))
            {
                backupFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Backups");
            }

            if (!Directory.Exists(backupFolderPath))
            {
                Directory.CreateDirectory(backupFolderPath);
            }

            var builder = new SqlConnectionStringBuilder(_connectionString);
            string databaseName = string.IsNullOrEmpty(builder.InitialCatalog) ? "ElectronicsStoreDB" : builder.InitialCatalog;
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string backupFilePath = Path.Combine(backupFolderPath, $"{databaseName}_{timestamp}.bak");

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

        public bool RestoreDatabase(string backupFilePath)
        {
            if (!File.Exists(backupFilePath))
            {
                throw new FileNotFoundException("Backup file not found.", backupFilePath);
            }

            var builder = new SqlConnectionStringBuilder(_connectionString);
            string databaseName = string.IsNullOrEmpty(builder.InitialCatalog) ? "ElectronicsStoreDB" : builder.InitialCatalog;

            // Connect to master database to avoid connection locking on the target DB
            builder.InitialCatalog = "master";
            string masterConnectionString = builder.ConnectionString;

            string sql = $@"
            ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            RESTORE DATABASE [{databaseName}] FROM DISK = @backupFile WITH REPLACE;
            ALTER DATABASE [{databaseName}] SET MULTI_USER;";

            using (SqlConnection conn = new SqlConnection(masterConnectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@backupFile", backupFilePath);
                cmd.CommandTimeout = 300; // 5 mins timeout for large restores
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}
