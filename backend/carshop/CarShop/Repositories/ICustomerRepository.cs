using CarShop.HandlerQueryStrings;
using CarShop.Models;

namespace CarShop.Repositories;

public interface ICustomerRepository : IRepository<CustomerDB> {
    public IEnumerable<CustomerDB> GetCustomersWithFilter(CustomerQueryFilter filter);
}