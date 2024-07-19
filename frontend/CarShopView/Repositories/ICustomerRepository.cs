using System.Threading.Tasks;
using Refit;
using CarShopView.Models;

namespace CarShopView.Repositories;

public interface ICustomerRepository {
    [Get("/clientes")]
    Task<ApiResponse<IEnumerable<Customer>>> GetCustomers();

    [Get("/clientes")]
    Task<ApiResponse<IEnumerable<Customer>>> GetCustomers(IQueryCustomers filter);

    [Post("/clientes")]
    Task<ApiResponse<Customer>> PostCustomer([Body] ICustomer customer);

    [Put("/clientes/{id}")]
    Task<ApiResponse<IEnumerable<Customer>>> PutCustomer(int id, [Body] ICustomer customer);

    [Delete("/clientes/{id}")]
    Task<ApiResponse<Customer>> DeleteCustomer(int id);
}