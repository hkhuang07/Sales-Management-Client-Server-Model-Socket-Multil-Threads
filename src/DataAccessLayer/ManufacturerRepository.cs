using ElectronicsStore.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElectronicsStore.DataAccess
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly ElectronicsStoreContext _context;

        // Constructor đã được cập nhật để nhận DbContext qua Dependency Injection
        public ManufacturerRepository(ElectronicsStoreContext context)
        {
            _context = context;
        }

        //Tra cứu
        public List<Manufacturers> GetAll() => _context.Manufacturer.ToList();

        public Manufacturers? GetById(int id) => _context.Manufacturer.Find(id);

        //Thêm mới
        public void Add(Manufacturers manufacturer)
        {
            _context.Manufacturer.Add(manufacturer);
            // _context.SaveChanges(); // Đã xóa, vì Service sẽ gọi SaveChanges
        }

        //Cập nhật
        public void Update(Manufacturers manufacturer)
        {
            var existingManufacture = _context.Manufacturer.Find(manufacturer.ID);
            if (existingManufacture != null)
            {
                existingManufacture.ManufacturerName = manufacturer.ManufacturerName;
                existingManufacture.ManufacturerAddress = manufacturer.ManufacturerAddress;
                existingManufacture.ManufacturerPhone = manufacturer.ManufacturerPhone;
                existingManufacture.ManufacturerEmail = manufacturer.ManufacturerEmail;

                _context.Manufacturer.Update(existingManufacture);
                // _context.SaveChanges(); // Đã xóa, vì Service sẽ gọi SaveChanges
            }
            else
            {
                throw new Exception($"Manufacturer with ID = {manufacturer.ID} not found.");
            }
        }

        //Xóa
        public void Delete(Manufacturers manufacturer)
        {
            _context.Manufacturer.Remove(manufacturer);
            // _context.SaveChanges(); // Đã xóa, vì Service sẽ gọi SaveChanges
        }
    }
}