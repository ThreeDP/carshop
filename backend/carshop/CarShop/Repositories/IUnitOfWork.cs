using CarShop.Models;

namespace CarShop.Repositories;

public interface IUnitOfWork {
    ICustomerRepository?                    CustomerRepository { get; }
    ITransactionRepository?                 TransactionRepository { get; }
    IVehicleRepository?                     VehicleRepository { get; }
    IRepository<VehicleImageDB>?            VehicleImageRepository { get; }

    void Commit();
}