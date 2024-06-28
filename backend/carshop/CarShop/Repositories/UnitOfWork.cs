using System.Security.AccessControl;
using CarShop.Context;
using CarShop.Models;

namespace CarShop.Repositories;

public class UnitOfWork : IUnitOfWork {
    private ICustomerRepository?                     _customerRepo;
    private ITransactionRepository?                 _transactionRepo;
    private IVehicleRepository?                      _vehicleRepo;
    private IRepository<VehicleImageDB>?             _vehicleImageRepo;
    public CarShopDataContext _ctx;

    public UnitOfWork(CarShopDataContext context) {
        _ctx = context;
    }

    public ICustomerRepository CustomerRepository {
        get {
            return _customerRepo = _customerRepo ?? new CustomerRepository(_ctx);
        }
    }

    public ITransactionRepository TransactionRepository {
        get {
            return _transactionRepo = _transactionRepo ?? new TransactionRepository(_ctx); 
        }
    }

    public IVehicleRepository VehicleRepository {
        get {
            return _vehicleRepo = _vehicleRepo ?? new VehicleRepository(_ctx);
        }
    }

    public IRepository<VehicleImageDB> VehicleImageRepository {
        get {
            return _vehicleImageRepo = _vehicleImageRepo ?? new Repository<VehicleImageDB>(_ctx);
        }
    }

    public void Commit() {
        _ctx.SaveChanges();
    }
}