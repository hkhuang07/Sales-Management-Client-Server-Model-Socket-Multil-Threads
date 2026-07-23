using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;

namespace ElectronicsStore.BusinessLogic
{
    public class ManufacturerService
    {
        private readonly IManufacturerRepository _repository;
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork; // Thêm UnitOfWork


        // Constructor đã được cập nhật để nhận IManufacturerRepository qua DI
        public ManufacturerService(IManufacturerRepository repository, IMapper mapper, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //Kiểm tra dữ liệu đầu vào
        private void Validate(ManufacturerDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Manufacturer data cannot be null.");

            if (string.IsNullOrWhiteSpace(dto.ManufacturerName) || dto.ManufacturerName.Length > 100)
                throw new ArgumentException("Manufacturer name cannot be blank and maximum 100 characters.");

            if (string.IsNullOrWhiteSpace(dto.ManufacturerAddress) || dto.ManufacturerAddress.Length > 200)
                throw new ArgumentException("Address cannot be blank and maximum 200 characters.");

            if (!IsValidPhone(dto.ManufacturerPhone))
                throw new ArgumentException("Invalid phone number. Valid example: 0901234567.");

            if (!IsValidEmail(dto.ManufacturerEmail))
                throw new ArgumentException("Invalid email address.");
        }

        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            var regex = new Regex(@"^(0|\+84)(\d{9})$");
            return regex.IsMatch(phone);
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        //Tra cứu
        public List<ManufacturerDTO> GetAll()
        {
            var list = _repository.GetAll();
            return _mapper.Map<List<ManufacturerDTO>>(list);
        }

        public ManufacturerDTO GetById(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new Exception($"Manufacturer not found with ID = {id}.");
            return _mapper.Map<ManufacturerDTO>(entity);
        }

        public List<ManufacturerDTO> GetByName(string name)
        {
            var list = _repository.GetAll();
            var result = list.FindAll(m => m.ManufacturerName.Contains(name, StringComparison.OrdinalIgnoreCase));
            return _mapper.Map<List<ManufacturerDTO>>(result);
        }

        //Thêm mới
        public void Add(ManufacturerDTO dto)
        {
            Validate(dto);
            var entity = _mapper.Map<Manufacturers>(dto);
            _repository.Add(entity);
            _unitOfWork.SaveChanges();
        }

        //Cập nhật
        public void Update(int id, ManufacturerDTO dto)
        {
            Validate(dto);

            var entity = _repository.GetById(id);
            if (entity == null)
                throw new Exception($"Manufacturer not found with ID = {id}.");

            entity.ManufacturerName = dto.ManufacturerName;
            entity.ManufacturerAddress = dto.ManufacturerAddress;
            entity.ManufacturerPhone = dto.ManufacturerPhone;
            entity.ManufacturerEmail = dto.ManufacturerEmail;

            _repository.Update(entity);
            _unitOfWork.SaveChanges();

        }

        //Xóa
        public void Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new Exception($"Manufacturer not found with ID = {id}.");

            _repository.Delete(entity);
            _unitOfWork.SaveChanges();

        }
    }
}