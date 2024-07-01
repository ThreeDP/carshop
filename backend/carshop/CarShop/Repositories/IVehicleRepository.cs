using CarShop.HandlerQueryStrings;
using CarShop.Models;
using HandlerQueryStrings;

namespace CarShop.Repositories;

public interface IVehicleRepository : IRepository<VehicleDB> {
    public PagedList<VehicleDB> GetVehiclesWithFilter(VehicleQueryFilter filter);
}