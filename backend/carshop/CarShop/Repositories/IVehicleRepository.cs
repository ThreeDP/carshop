using CarShop.HandlerQueryStrings;
using CarShop.Models;

namespace CarShop.Repositories;

public interface IVehicleRepository : IRepository<VehicleDB> {
    public IEnumerable<VehicleDB> GetVehiclesWithFilter(VehicleQueryFilter filter);
}