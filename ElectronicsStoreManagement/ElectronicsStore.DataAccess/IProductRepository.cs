using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataAccess
{
    public interface IProductRepository
    {
        List<Products> GetAll();
        Products? GetById(int id);
        Products? Get1ByName(string key);
        List<Products> GetByName(string key);

        void Add(Products product);
        void Update(Products product);
        void UpdateImage(int productId, string imageFileName);

        void Delete(Products product);
        List<Products> GetAllWithCategoryManufacturer();

    }
}
