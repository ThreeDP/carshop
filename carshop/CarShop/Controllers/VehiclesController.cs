using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarShop.Context;
using CarShop.Models;
using CarShop.Filters;

namespace CarShop.Controllers;

[ApiController]
[Route("vehicles")]
public class VehiclesController : ControllerBase
{
    private readonly CarShopDataContext _ctx;

    public VehiclesController(CarShopDataContext context) {
        _ctx = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VehicleDB>> Get() {
        var v = _ctx.Vehicles.AsNoTracking().Take(10).ToList();
        if (v is null) {
            return NotFound();
        }
        return v;
    }

    [HttpGet("{id:int:min(1)}", Name="obterveiculo")]
    public ActionResult<VehicleDB> Get(int id) {
        var v = _ctx.Vehicles.AsNoTracking().FirstOrDefault(v => v.VehicleDBId == id);
        if (v is null) {
            return NotFound();
        }
        return v;
    }


    [HttpPost]
    public ActionResult Post([FromBody] VehicleDB v) {
        if (!ModelState.IsValid || v is null)
            return BadRequest(ModelState);
        _ctx.Vehicles.Add(v);
        _ctx.SaveChanges();
        return new CreatedAtRouteResult("obterveiculo",
            new { id = v.VehicleDBId}, v);
    }
}