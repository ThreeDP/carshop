using System.Threading.Tasks;
using Refit;
using CarShopView.Models;

namespace CarShopView.Repositories;



public interface ICustomerRepository {
    [Get("/clientes")]
    Task<ApiResponse<IEnumerable<Customer>>> GetCustomers();

    [Get("/clientes")]
    Task<ApiResponse<IEnumerable<Customer>>> GetCustomers(IQueryCustomers filter);
}