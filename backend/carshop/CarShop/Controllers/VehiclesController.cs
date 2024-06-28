using Microsoft.AspNetCore.Mvc;
using CarShop.Models;
using CarShop.Repositories;
using CarShop.DTO;
using CarShop.HandlerQueryStrings;

namespace CarShop.Controllers;

[ApiController]
[Route("veiculos")]
public class VehiclesController : ControllerBase
{
    private readonly IUnitOfWork _unitDB;

    public VehiclesController(IUnitOfWork uow) {
        _unitDB = uow;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VehicleDTO>> GetVehicles([FromQuery] VehicleQueryFilter filter) {
        var vehicles = _unitDB.VehicleRepository.GetVehiclesWithFilter(filter);
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

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<VehicleDTO> PutVehicle(int id, [FromBody] VehicleDTO v) {
        if (v is null) {
            return BadRequest();
        }
        var vehicle = _unitDB.VehicleRepository.Update(new VehicleDB(v));
        _unitDB.Commit();
        return Ok(new VehicleDTO(vehicle));
    }

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