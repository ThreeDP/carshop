using System.Threading.Tasks;
using Refit;
using CarShopView.Models;
using CarShopView.Querys;

namespace CarShopView.Repositories;

public interface ICustomerRepository {
    [Get("/clientes")]
    Task<ApiResponse<IEnumerable<Customer>>> GetCustomers();

    [Get("/clientes")]
    Task<ApiResponse<IEnumerable<Customer>>> GetCustomers(IQueryCustomers filter, [Authorize("Bearer")] string token);

    [Post("/clientes")]
    Task<ApiResponse<Customer>> PostCustomer([Body] ICustomer customer, [Authorize("Bearer")] string token);

    [Put("/clientes/{id}")]
    Task<ApiResponse<IEnumerable<Customer>>> PutCustomer(int id, [Body] ICustomer customer, [Authorize("Bearer")] string token);

    [Delete("/clientes/{id}")]
    Task<ApiResponse<Customer>> DeleteCustomer(int id, [Authorize("Bearer")] string token);
}