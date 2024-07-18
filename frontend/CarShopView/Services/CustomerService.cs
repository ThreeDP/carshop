using CarShopView.Models;
using CarShopView.Repositories;

namespace CarShopView.Services;

public class CustomerService {
    public int                          CurrentPageNumber { get; set; } = 1;
    public QueryCustomers?              Querys { get; set; }
    public PaginationHeader?            Pagination { get; set; }
    public IEnumerable<Customer>?       CustomerList { get; set; }
    public Customer?                    ActualCustomer { get; set; }
}