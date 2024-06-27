using CarShop.Models;

namespace CarShop.Repositories;

public interface IUnitOfWork {
    IRepository<CustomerDB>                 CustomerRepository { get; }
    IRepository<FinancialTransactionsDB>    TransactionRepository { get; }
    IRepository<VehicleDB>                  VehicleRepository { get; }
    IRepository<VehicleImageDB>             VehicleImageRepository { get; }

    void Commit();
}