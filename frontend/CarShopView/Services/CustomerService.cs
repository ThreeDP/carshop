using CarShopView.Models;
using CarShopView.Repositories;

namespace CarShopView.Services;

public interface ICustomerService {
    public int                          CurrentPageNumber { get; set; }
    public IQueryCustomers?             Querys { get; set; }
    public IPaginationHeader?           Pagination { get; set; }
    public IEnumerable<ICustomer>?      CustomerList { get; set; }
    public ICustomer?                   ActualCustomer { get; set; }
}

public class CustomerService : ICustomerService {
    public int                          CurrentPageNumber { get; set; } = 1;
    public CustomerService(
        IQueryCustomers qc,
        IPaginationHeader ph,
        ICustomer c) {
        Querys = qc;
        Pagination = ph;
        ActualCustomer = c;
        CustomerList = new List<Customer>();
    }
    public IQueryCustomers?              Querys { get; set; }
    public IPaginationHeader?            Pagination { get; set; }
    public IEnumerable<ICustomer>?       CustomerList { get; set; }
    public ICustomer?                    ActualCustomer { get; set; }
}