using CarShop.Context;
using CarShop.Models;

namespace CarShop.Repositories;

public class CustomerRepository : Repository<CustomerDB>, ICustomerRepository {

    public CustomerRepository(CarShopDataContext context) : base(context) {
        
    }

}