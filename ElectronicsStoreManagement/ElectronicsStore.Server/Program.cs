using AutoMapper;
using ElectronicsStore.BusinessLogic;
using ElectronicsStore.DataAccess;
using ElectronicsStore.Server;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

public class Program
{
    private const int PORT = 301;
    private static IMapper _mapper;
    private static DbContextOptions<ElectronicsStoreContext> _dbContextOptions;

    static async Task Main(string[] args)
    {
        // 1. Cấu hình DbContextOptions
        // Sửa lỗi chuỗi thoát và thêm tên instance SQL Server
        string connectionString = "Server=.;Database=ElectronsStore;Integrated Security=True;MultipleActiveResultSets=True; TrustServerCertificate=True";

        _dbContextOptions = new DbContextOptionsBuilder<ElectronicsStoreContext>()
            .UseSqlServer(connectionString)
            .Options;

        // 2. Khởi tạo AutoMapper
        _mapper = MapperConfig.Initialize();

        TcpListener listener = null;
        try
        {
            listener = new TcpListener(IPAddress.Any, PORT);
            listener.Start();
            Console.WriteLine($"Server started. Listening on port {PORT}...");

            while (true)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                Console.WriteLine($"Client connected: {client.Client.RemoteEndPoint}");

                // Khởi chạy tác vụ xử lý client, truyền vào các dependency đã được khởi tạo
                _ = Task.Run(() => ServerHandler.HandleClientAsync(client, _mapper, _dbContextOptions));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Server error: {ex.Message}");
        }
        finally
        {
            listener?.Stop();
        }
    }
}