using ElectronicsStore.BusinessLogic;
using ElectronicsStore.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Database Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=.;Database=ElectronicsStoreDB;Integrated Security=True;MultipleActiveResultSets=True;TrustServerCertificate=True";

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuditInterceptor>();
builder.Services.AddDbContext<ElectronicsStoreContext>((sp, options) => {
    var interceptor = sp.GetRequiredService<AuditInterceptor>();
    options.UseSqlServer(connectionString)
           .AddInterceptors(interceptor);
});

// Register Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register AutoMapper
builder.Services.AddAutoMapper(cfg => {
    cfg.AddProfile<ElectronicsStore.BusinessLogic.MappingProfile>();
});

// Configure JWT Authentication
var jwtSecret = builder.Configuration["Jwt:Key"] ?? "super_secret_key_electronic_store_12345!@#";
var key = Encoding.ASCII.GetBytes(jwtSecret);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<ElectronicsStore.Server.Hubs.StoreHub>("/storeHub");

app.Run();