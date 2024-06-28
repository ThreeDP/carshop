using CarShop.Context;
using CarShop.HandlerQueryStrings;
using CarShop.Models;

namespace CarShop.Repositories;

public class CustomerRepository : Repository<CustomerDB>, ICustomerRepository {

    public CustomerRepository(CarShopDataContext context) : base(context) {
        
    }

    public IEnumerable<CustomerDB> GetCustomersWithFilter(CustomerQueryFilter filter) {
        var customers = _ctx.Customers?.OrderBy(c => c.Name).AsQueryable();
        if (filter.docType is not null) {
            customers = customers.Where(c => c.DocType == filter.docType);
        }
        if (filter.name is not null) {
            customers = customers.Where(c => c.Name.StartsWith(filter.name));
        }
        customers = customers.Skip((filter.PageNumber - 1) * filter.PageSize);
        customers = customers.Take(filter.PageSize);
        return customers.ToList();
    }

}