using CarShop.HandlerQueryStrings;
using CarShop.Models;
using HandlerQueryStrings;

namespace CarShop.Repositories;

public interface ICustomerRepository : IRepository<CustomerDB> {
    public PagedList<CustomerDB> GetCustomersWithFilter(CustomerQueryFilter filter);
}