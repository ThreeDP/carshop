using Microsoft.AspNetCore.Mvc;
using CarShop.Models;
using CarShop.Repositories;
using CarShop.DTO;

namespace CarShop.Controllers;

[ApiController]
[Route("veiculos")]
public class VehiclesController : ControllerBase
{
    private readonly IUnitOfWork _unitDB;

    public VehiclesController(IUnitOfWork uow) {
        _unitDB = uow;
    }

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<VehicleDB>>> GetAsync(
    //     string? renavan, string? licensePlate, string? brand,
    //     string? model, DateTime? modelYear, string? vehicleType,
    //     string? situation, int range=10
    // ) {
    //     var vehicle = _ctx.Vehicles?.OrderBy(v => v.Model).AsQueryable();

    //     if (renavan is not null)
    //         vehicle = vehicle.Where( v => v.Renavan.StartsWith(renavan));
    //     if (licensePlate is not null)
    //         vehicle = vehicle.Where( v => v.LicensePlate.StartsWith(licensePlate));
    //     if (brand is not null)
    //         vehicle = vehicle.Where( v => v.Brand == brand);
    //     if (model is not null)
    //         vehicle = vehicle.Where( v => v.Model == model);
    //     if (model is not null)
    //         vehicle = vehicle.Where( v => v.ModelYear == modelYear);
    //     if (vehicleType is not null)
    //         vehicle = vehicle.Where( v => v.VehicleType == vehicleType);
    //     if (situation is not null)
    //         vehicle = vehicle.Where( v => v.Situation == situation);
            
    //     var res = await vehicle.Take(range)
    //         .Include(v => v.VehicleImages)
    //         .ToListAsync();
    //     if (res is null) {
    //         return NotFound();
    //     }
    //     return Ok(res);
    // }

    [HttpGet]
    public ActionResult<IEnumerable<VehicleDTO>> GetVehicles() {
        var vehicles = _unitDB.VehicleRepository.GetAll();
        var responseVehicles = vehicles.Select(v => new VehicleDTO(v)).ToList();
        return Ok(responseVehicles);
    }

    [HttpGet("{id:int:min(1)}", Name="obter-veiculo")]
    public ActionResult<VehicleDTO> GetVehicle(int id) {
        var vehicle = _unitDB.VehicleRepository.Get(v => v.Id == id);
        if (vehicle is null) {
            return NotFound();
        }
        return Ok(new VehicleDTO(vehicle));
    }

    // [HttpGet("{id:int:min(1)}", Name="obter-veiculo")]
    // public async Task<ActionResult<VehicleDB>> GetAsync(int id) {
    //     var v = await _ctx.Vehicles?
    //         .AsNoTracking()
    //         .Include(v => v.VehicleImages)
    //         .FirstOrDefaultAsync(v => v.Id == id);
    //     if (v is null) {
    //         return NotFound();
    //     }
    //     return Ok(v);
    // }
    [HttpPost]
    public ActionResult<VehicleDTO> PostVehicle([FromBody] VehicleDTO v) {
        if (v is null) {
            return BadRequest();
        }
        var newVehicle = _unitDB.VehicleRepository.Add(new VehicleDB(v));
        var responseVehicle = new VehicleDTO(newVehicle);
        _unitDB.Commit();
        return new CreatedAtRouteResult("obter-veiculo",
            new {id = responseVehicle.Id}, responseVehicle);
    }

    // [HttpPost]
    // public ActionResult Post([FromBody] VehicleDB v) {
    //     if (!ModelState.IsValid || v is null)
    //         return BadRequest(ModelState);
    //     _ctx.Vehicles?.Add(v);
    //     _ctx.SaveChanges();
    //     return new CreatedAtRouteResult("obter-veiculo",
    //         new { id = v.Id}, v);
    // }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<VehicleDTO> PutVehicle(int id, [FromBody] VehicleDTO v) {
        if (v is null) {
            return BadRequest();
        }
        var vehicle = _unitDB.VehicleRepository.Update(new VehicleDB(v));
        _unitDB.Commit();
        return Ok(new VehicleDTO(vehicle));
    }

    // [HttpPut("{id:int}")]
    // public ActionResult Put(int id, [FromBody] VehicleDB v) {
    //     if (id != v.Id || !ModelState.IsValid || v is null) {
    //         return BadRequest();
    //     }
    //     _ctx.Entry(v).State = EntityState.Modified;
    //     _ctx.SaveChanges();
    //     return Ok(v);
    // }

    // [HttpDelete("{id:int}")]
    // public ActionResult Delete(int id) {
    //     var vehicle = _ctx.Vehicles?.FirstOrDefault(v => v.Id == id);
    //     if (vehicle is null) {
    //         return NotFound("Veiculo não encontrado.");
    //     }
    //     _ctx.Vehicles?.Remove(vehicle);
    //     _ctx.SaveChanges();
    //     return Ok(vehicle);
    // }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<VehicleDTO> DeleteVehicle(int id) {
        var vehicleToDel = _unitDB.VehicleRepository.Get(v => v.Id == id);
        if (vehicleToDel is null) {
            return BadRequest();
        }
        var vehicle = _unitDB.VehicleRepository.Delete(vehicleToDel);
        _unitDB.Commit();
        return Ok(new VehicleDTO(vehicle));
    }
}