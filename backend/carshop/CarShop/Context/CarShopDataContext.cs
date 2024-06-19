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
    }

    public CarShopDataContext(){}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Conf.GetConnectionString("defaultConn"));
    }

    public virtual DbSet<CustomerDB>? Customers { get; set; }
    public virtual DbSet<FinancialTransactionsDB>? FinancialTransactions { get; set; }
    public virtual DbSet<VehicleImageDB>? VehicleImages { get; set; }
    public virtual DbSet<VehicleDB>? Vehicles { get; set; }
}
