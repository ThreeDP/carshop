using CarShop.Context;
using CarShop.HandlerQueryStrings;
using CarShop.Models;
using HandlerQueryStrings;

namespace CarShop.Repositories;

public class CustomerRepository : Repository<CustomerDB>, ICustomerRepository {

    public CustomerRepository(CarShopDataContext context) : base(context) {
        
    }

    public PagedList<CustomerDB> GetCustomersWithFilter(CustomerQueryFilter filter) {
        var customers = _ctx.Customers?.OrderBy(c => c.Name).AsQueryable();
        if (filter.docType is not null) {
            customers = customers.Where(c => c.DocType == filter.docType);
        }
        if (filter.name is not null) {
            customers = customers.Where(c => c.Name.StartsWith(filter.name));
        }
        return PagedList<CustomerDB>.ToPagedList(customers, filter.PageNumber, filter.PageSize);
    }

}