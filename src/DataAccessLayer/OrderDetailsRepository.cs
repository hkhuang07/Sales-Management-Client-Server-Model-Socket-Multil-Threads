using ElectronicsStore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class OrderDetailsRepository : IOrderDetailsRepository
{
    private readonly ElectronicsStoreContext _context;

    public OrderDetailsRepository(ElectronicsStoreContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Lấy danh sách chi tiết đơn hàng theo mã đơn hàng
    public List<Order_Details> GetByOrderID(int orderId)
    {
        return _context.Order_Details
            .Where(d => d.OrderID == orderId)
            .Include(d => d.Product) // Có thể bạn cần include Product để lấy thông tin sản phẩm
            .ToList();
    }

    // Lấy một chi tiết đơn hàng theo ID
    public Order_Details? GetById(int id)
    {
        return _context.Order_Details
            .Include(d => d.Product)
            .FirstOrDefault(d => d.ID == id);
    }

    // Thêm một chi tiết đơn hàng
    public void Insert(Order_Details entity)
    {
        _context.Order_Details.Add(entity);
    }

    // Thêm nhiều chi tiết đơn hàng
    public void AddRange(List<Order_Details> entities)
    {
        _context.Order_Details.AddRange(entities);
    }

    // Cập nhật một chi tiết đơn hàng
    public void Update(Order_Details entity)
    {
        _context.Order_Details.Update(entity);
    }

    // Xóa một chi tiết đơn hàng
    public void Delete(Order_Details detail)
    {
        _context.Order_Details.Remove(detail);
    }

    // Xóa tất cả chi tiết đơn hàng theo mã đơn hàng
    public void DeleteByOrderID(int orderId)
    {
        var details = _context.Order_Details.Where(d => d.OrderID == orderId).ToList();
        if (details.Any())
        {
            _context.Order_Details.RemoveRange(details);
        }
    }
}