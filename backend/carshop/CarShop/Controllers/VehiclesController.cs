using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarShop.Context;
using CarShop.Models;
using CarShop.Filters;

namespace CarShop.Controllers;

[ApiController]
[Route("veiculos")]
public class VehiclesController : ControllerBase
{
    private readonly CarShopDataContext _ctx;

    public VehiclesController(CarShopDataContext context) {
        _ctx = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VehicleDB>>> GetAsync(
        string? renavan, string? licensePlate, string? brand,
        string? model, DateTime? modelYear, string? vehicleType,
        string? situation, int range=10
    ) {
        var v = await _ctx.Vehicles?
            .AsNoTracking()
            .OrderBy(v => v.Model)
            .Where( v => string.IsNullOrEmpty(renavan) || v.Renavan.StartsWith(renavan))
            .Where( v => string.IsNullOrEmpty(licensePlate) || v.LicensePlate.StartsWith(licensePlate))
            .Where( v => string.IsNullOrEmpty(brand) || v.Brand == brand)
            .Where( v => string.IsNullOrEmpty(model) || v.Model == model)
            .Where( v => modelYear == null || v.ModelYear == modelYear)
            .Where( v => string.IsNullOrEmpty(vehicleType) || v.VehicleType == vehicleType)
            .Where( v => string.IsNullOrEmpty(situation) || v.Situation == situation)
            .Take(range)
            .Include(v => v.VehicleImages)
            .ToListAsync();
        if (v is null) {
            return NotFound();
        }
        return Ok(v);
    }

    [HttpGet("{id:int:min(1)}", Name="obter-veiculo")]
    public async Task<ActionResult<VehicleDB>> GetAsync(int id) {
        var v = await _ctx.Vehicles?
            .AsNoTracking()
            .Include(v => v.VehicleImages)
            .FirstOrDefaultAsync(v => v.Id == id);
        if (v is null) {
            return NotFound();
        }
        return Ok(v);
    }

    [HttpPost]
    public ActionResult Post([FromBody] VehicleDB v) {
        if (!ModelState.IsValid || v is null)
            return BadRequest(ModelState);
        _ctx.Vehicles?.Add(v);
        _ctx.SaveChanges();
        return new CreatedAtRouteResult("obter-veiculo",
            new { id = v.Id}, v);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, [FromBody] VehicleDB v) {
        if (id != v.Id || !ModelState.IsValid || v is null) {
            return BadRequest();
        }
        _ctx.Entry(v).State = EntityState.Modified;
        _ctx.SaveChanges();
        return Ok(v);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id) {
        var vehicle = _ctx.Vehicles?.FirstOrDefault(v => v.Id == id);
        if (vehicle is null) {
            return NotFound("Veiculo não encontrado.");
        }
        _ctx.Vehicles?.Remove(vehicle);
        _ctx.SaveChanges();
        return Ok(vehicle);
    }
}