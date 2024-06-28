using CarShop.Context;
using CarShop.HandlerQueryStrings;
using CarShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Repositories;

public class VehicleRepository : Repository<VehicleDB>, IVehicleRepository {
    public VehicleRepository(CarShopDataContext context) : base(context) {

    }

    public IEnumerable<VehicleDB> GetVehiclesWithFilter(VehicleQueryFilter filter) {
        var vehicles = _ctx.Vehicles?.AsQueryable();
        if (filter.Model is not null) {
            vehicles = vehicles.Where(v => v.Model == filter.Model);
        }
        if (filter.Brand is not null) {
            vehicles = vehicles.Where(v => v.Brand == filter.Brand);
        }
        if (filter.ModelYear is not null) {
            vehicles = vehicles.Where(v => v.ModelYear == filter.ModelYear);
        }
        if (filter.Renavan is not null) {
            vehicles = vehicles.Where(v => v.Renavan.StartsWith(filter.Renavan));
        }
        if (filter.LicensePlate is not null) {
            vehicles = vehicles.Where(v => v.LicensePlate.StartsWith(filter.LicensePlate));
        }
        if (filter.Situation is not null) {
            vehicles = vehicles.Where(v => v.Situation == filter.Situation);
        }
        if (filter.VehicleType is not null) {
            vehicles = vehicles.Where(v => v.VehicleType == filter.VehicleType);
        }
        vehicles = vehicles.Skip((filter.PageNumber - 1) * filter.PageNumber);
        vehicles = vehicles.Take(filter.PageSize);
        vehicles = vehicles.Include(v => v.VehicleImages);
        return vehicles.ToList();
    }
}