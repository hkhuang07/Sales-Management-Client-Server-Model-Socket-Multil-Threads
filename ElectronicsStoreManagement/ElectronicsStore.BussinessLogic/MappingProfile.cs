using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ElectronicsStore.DataAccess;
using ElectronicsStore.DataTransferObject;

namespace ElectronicsStore.BusinessLogic
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Ánh xạ từ Entity -> DTO
            CreateMap<Categories, CategoryDTO>().ReverseMap();
            CreateMap<Employees, EmployeeDTO>().ReverseMap();
            CreateMap<Customers, CustomerDTO>().ReverseMap();
            CreateMap<Manufacturers, ManufacturerDTO>().ReverseMap();
            CreateMap<Products, ProductDTO>().ReverseMap();
            CreateMap<Orders, OrderDTO>().ReverseMap();
            CreateMap<Order_Details, OrderDetailsDTO>().ReverseMap();

            // Products → ProductList (hiển thị dữ liệu ra giao diện)
            CreateMap<Products, ProductList>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.ManufacturerName, opt => opt.MapFrom(src => src.Manufacturer.ManufacturerName));

           /* CreateMap<Products, ImageUploadRequestDTO>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.Image))
                .ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => src.Image));*/

            // Orders → OrderList (hiển thị dữ liệu ra giao diện)
            CreateMap<Orders, OrderList>()
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.CustomerName))
                .ForMember(dest => dest.CustomeAddress, opt => opt.MapFrom(src => src.Customer.CustomerAddress))
                .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Customer.CustomerPhone))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.ViewDetails.Sum(d => d.Quantity * d.Price)));

            // OrderDTO ↔ Orders (dùng cho lưu/chỉnh sửa)
            CreateMap<Order_Details, OrderDetailsList>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Quantity * src.Price));
            CreateMap<OrderDetailsList, Order_Details>();

           CreateMap<Employees, LoginResponseDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Role));

            CreateMap<Employees, LoginRequestDTO>()
               .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
               .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password));
        }

    }
}
