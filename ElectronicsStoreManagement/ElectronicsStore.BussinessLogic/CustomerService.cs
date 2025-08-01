﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;

namespace ElectronicsStore.BusinessLogic
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        // Constructor đã được cập nhật để nhận ICustomerRepository qua DI
        public CustomerService(ICustomerRepository repository, IMapper mapper, UnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        //Kiểm tra dữ liệu đầu vào
        private void Validate(CustomerDTO dto)
        {
            if (dto == null)
                throw new ArgumentException("Customer data cannot be null.");

            if (string.IsNullOrWhiteSpace(dto.CustomerName) || dto.CustomerName.Length > 100)
                throw new ArgumentException("Customer name cannot be blank and maximum 100 characters.");

            if (string.IsNullOrWhiteSpace(dto.CustomerName) || dto.CustomerName.Length > 200)
                throw new ArgumentException("Address cannot be blank and maximum 200 characters.");

            if (!IsValidPhone(dto.CustomerPhone))
                throw new ArgumentException("Invalid phone number. Valid example: 0901234567.");

            if (!IsValidEmail(dto.CustomerEmail))
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
        public List<CustomerDTO> GetAll()
        {
            var list = _repository.GetAll();
            return _mapper.Map<List<CustomerDTO>>(list);
        }

        public CustomerDTO GetById(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null)
                throw new Exception($"Customer not found with ID = {id}.");
            return _mapper.Map<CustomerDTO>(entity);
        }

        public List<CustomerDTO> GetByName(string name)
        {
            var list = _repository.GetAll().Where(c => c.CustomerName.Contains(name)).ToList();
            return _mapper.Map<List<CustomerDTO>>(list);
        }

        //Thêm mới
        public void Add(CustomerDTO dto)
        {
            Validate(dto);

            var entity = _mapper.Map<Customers>(dto);
            _repository.Add(entity);
            _unitOfWork.SaveChanges();
        }
        public CustomerDTO AddReturn(CustomerDTO dto)
        {
            Validate(dto);

            var entity = _mapper.Map<Customers>(dto);
            _repository.Add(entity);
            _unitOfWork.SaveChanges(); // <-- ID sẽ được gán cho entity sau khi save

            // Map entity đã được cập nhật ID trở lại DTO và trả về
            var addedDto = _mapper.Map<CustomerDTO>(entity);
            return addedDto;
        }

        //Cập nhật
        public void Update(int id, CustomerDTO dto)
        {
            Validate(dto);

            var entity = _repository.GetById(id);
            if (entity == null) 
                throw new Exception($"Customer not found with ID = {id}.");

            entity.CustomerName = dto.CustomerName;
            entity.CustomerAddress = dto.CustomerAddress;
            entity.CustomerPhone = dto.CustomerPhone;
            entity.CustomerEmail = dto.CustomerEmail;
            _repository.Update(entity);
            _unitOfWork.SaveChanges();

        }

        //Xóa
        public void Delete(int id)
        {
            var entity = _repository.GetById(id);
            if (entity == null) throw new Exception($"Customer not found with ID = {id}.");
            _repository.Delete(entity);
            _unitOfWork.SaveChanges();

        }
    }
}