using CarShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CarShop.Context;

public class CarShopDataContext : DbContext
{
    protected readonly IConfiguration Conf;

    public CarShopDataContext(IConfiguration conf)
    {
        this.Conf = conf;
        // Clients = new DbSet<ClientDB>();
        // FinancialTransactions = new DbSet<FinancialTransactionsDB>();
        // VehicleImages = new DbSet<VehicleImageDB>();
        // Vehicles = new DbSet<VehicleDB>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Conf.GetConnectionString("defaultConn"));
    }

    public DbSet<ClientDB>? Clients { get; set; }
    public DbSet<FinancialTransactionsDB>? FinancialTransactions { get; set; }
    public DbSet<VehicleImageDB>? VehicleImages { get; set; }
    public DbSet<VehicleDB>? Vehicles { get; set; }
}
