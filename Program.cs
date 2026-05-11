using Machine_Product_Service.DbContext;
using Microsoft.EntityFrameworkCore;
using Machine_Product_Service.IMachineProductRepository;
using Machine_Product_Service.IUserRepository;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<MachineRepository, MachineRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddDbContext
    <DBcontext>(options => options.UseNpgsql
        (builder.Configuration.
            GetConnectionString("ConnectionString")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/", () => "Hello World!");

app.Run();