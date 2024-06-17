using CarShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Context;

public abstract class IDataBaseContext : DbContext
{
    protected readonly IConfiguration Conf;
    public IDataBaseContext(IConfiguration conf) 
    {
        this.Conf = conf;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Conf.GetConnectionString("defaultConn"));
    }

    public virtual DbSet<ClientDB>? Clients { get; set; } 
    public virtual DbSet<FinancialTransactionsDB>? FinancialTransations { get; set; }
}