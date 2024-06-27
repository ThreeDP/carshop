using System.Security.AccessControl;
using CarShop.Context;
using CarShop.Models;

namespace CarShop.Repositories;

public class UnitOfWork : IUnitOfWork {
    private IRepository<CustomerDB>?                 _customerRepo;
    private IRepository<FinancialTransactionsDB>?    _transactionRepo;
    private IRepository<VehicleDB>?                  _vehicleRepo;
    private IRepository<VehicleImageDB>?             _vehicleImageRepo;
    public CarShopDataContext _ctx;

    public UnitOfWork(CarShopDataContext context) {
        _ctx = context;
    }

    public IRepository<CustomerDB> CustomerRepository {
        get {
            return _customerRepo = _customerRepo ?? new Repository<CustomerDB>(_ctx);
        }
    }

    public IRepository<FinancialTransactionsDB> TransactionRepository {
        get {
            return _transactionRepo = _transactionRepo ?? new Repository<FinancialTransactionsDB>(_ctx); 
        }
    }

    public IRepository<VehicleDB> VehicleRepository {
        get {
            return _vehicleRepo = _vehicleRepo ?? new Repository<VehicleDB>(_ctx);
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