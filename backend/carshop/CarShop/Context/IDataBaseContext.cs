// using CarShop.Models;
// using Microsoft.EntityFrameworkCore;

// namespace CarShop.Context;

// public interface IDataBaseContext : IDisposable
// {
//     public DbSet<CustomerDB>? Customers { get; set; } 
//     public DbSet<FinancialTransactionsDB>? FinancialTransactions { get; set; }
//     public DbSet<VehicleImageDB>? VehicleImages { get; set; }
//     public DbSet<VehicleDB>? Vehicles { get; set; }
//     IQueryable<TEntity> Query<TEntity>() where TEntity : class;
//     TEntity GetById<TEntity, TId>(TId id) where TEntity : class;
//     int SaveChanges();
// }