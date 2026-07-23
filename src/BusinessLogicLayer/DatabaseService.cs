using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectronicsStore.DataAccess;

namespace ElectronicsStore.BusinessLogic
{
    public class DatabaseService
    {
        private readonly DatabaseRepository _repository;

        public DatabaseService()
        {
            _repository = new DatabaseRepository();
        }

        public bool BackupDatabase(string backupFolderPath)
        {
            try
            {
                return _repository.BackupDatabase(backupFolderPath);
            }
            catch
            {
                return false;
            }
        }

        public bool RestoreDatabase(string backupFilePath)
        {
            try
            {
                return _repository.RestoreDatabase(backupFilePath);
            }
            catch
            {
                return false;
            }
        }
    }
}
