using System.Threading.Tasks;
using Refit;
using CarShopView.Models;

namespace CarShopView.Repositories;

public interface IVehicleRepository {
    [Get("/veiculos")]
    Task<ApiResponse<IEnumerable<Vehicle>>> GetVehicles([Authorize("Bearer")] string authorization);
}