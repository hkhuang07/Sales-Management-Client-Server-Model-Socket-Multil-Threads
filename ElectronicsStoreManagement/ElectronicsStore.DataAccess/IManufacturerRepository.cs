using ElectronicsStore.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataAccess
{
    public interface IManufacturerRepository
    {
        List<Manufacturers> GetAll();
        Manufacturers? GetById(int id);
        //public ManufacturerDTO GetById(string id);
        void Add(Manufacturers category);
        void Update(Manufacturers category);
        void Delete(Manufacturers manufacturer);

    }
}
