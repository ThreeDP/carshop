using CarShop.Context;
using CarShop.HandlerQueryStrings;
using CarShop.Models;
using HandlerQueryStrings;
using Microsoft.EntityFrameworkCore;

namespace CarShop.Repositories;

public class VehicleRepository : Repository<VehicleDB>, IVehicleRepository {
    public VehicleRepository(CarShopDataContext context) : base(context) {

    }

    public PagedList<VehicleDB> GetVehiclesWithFilter(VehicleQueryFilter filter) {
        var vehicles = _ctx.Vehicles.AsQueryable();
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
            vehicles = vehicles.Where(v => v.Renavan != null && v.Renavan.StartsWith(filter.Renavan));
        }
        if (filter.LicensePlate is not null) {
            vehicles = vehicles.Where(v => v.LicensePlate != null && v.LicensePlate.StartsWith(filter.LicensePlate));
        }
        if (filter.Situation is not null) {
            vehicles = vehicles.Where(v => v.Situation == filter.Situation);
        }
        if (filter.VehicleType is not null) {
            vehicles = vehicles.Where(v => v.VehicleType == filter.VehicleType);
        }
        vehicles = vehicles.Include(v => v.VehicleImages);
        return PagedList<VehicleDB>.ToPagedList(vehicles, filter.PageNumber, filter.PageSize);
    }
}