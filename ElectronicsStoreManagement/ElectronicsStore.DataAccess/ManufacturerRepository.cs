using ElectronicsStore.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsStore.DataAccess
{
    public class ManufacturerRepository: IManufacturerRepository
    {
        private readonly ElectronicsStoreContext _context;

        public ManufacturerRepository()
        {
            _context = new ElectronicsStoreContext();
        }


        //Tra cứu
        public List<Manufacturers> GetAll() => _context.Manufacturer.ToList();

        public Manufacturers? GetById(int id) => _context.Manufacturer.Find(id);


        //Thêm mới
        public void Add(Manufacturers manufacturer)
        {
            _context.Manufacturer.Add(manufacturer);
            _context.SaveChanges();
        }

        //Cập nhật
        public void Update(Manufacturers manufacturer)
        {
            var existingManufacture = _context.Manufacturer.Find(manufacturer.ID);
            if (existingManufacture != null)
            {
                // Cập nhật các thuộc tính cần thiết
                existingManufacture.ManufacturerName = manufacturer.ManufacturerName;
                existingManufacture.ManufacturerAddress = manufacturer.ManufacturerAddress;
                existingManufacture.ManufacturerPhone = manufacturer.ManufacturerPhone;
                existingManufacture.ManufacturerEmail = manufacturer.ManufacturerEmail;

                _context.Manufacturer.Update(existingManufacture);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"Manufacturer with ID = {manufacturer.ID} not found.");
            }
            _context.Manufacturer.Update(manufacturer);
            _context.SaveChanges();
        }

        //Xóa
        public void Delete(Manufacturers manufacturer)
        {
            _context.Manufacturer.Remove(manufacturer);
            _context.SaveChanges();
        }
    }
}
