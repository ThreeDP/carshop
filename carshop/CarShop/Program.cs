
using CarShop.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// string psqlConn = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<CarShopContext>(Options =>
//     Options.Npgsql(psqlConn,
//         ServerVersion.AutoDetect(psqlConn)
//     ));

builder.Services.AddDbContext<CarShopDataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("defaultConn")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
