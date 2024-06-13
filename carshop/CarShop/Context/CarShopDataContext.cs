using CarShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Context;

public class CarShopDataContext : DbContext
{
    protected readonly IConfiguration Conf;

    public CarShopDataContext(IConfiguration conf) 
    {
        this.Conf = conf;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Conf.GetConnectionString("defaultConn"));
    }

    public DbSet<ClientDB>? Clients { get; set; }
    public DbSet<FinancialTransactionsDB>? FinancialTransations { get; set; }
}
