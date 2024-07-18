using Microsoft.AspNetCore.Mvc;
using CarShop.Models;
using CarShop.Repositories;
using CarShop.DTO;
using CarShop.HandlerQueryStrings;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult<IEnumerable<VehicleDTO>> GetVehicles([FromQuery] VehicleQueryFilter filter) {
        var vehicles = _unitDB.VehicleRepository?.GetVehiclesWithFilter(filter);
        Response.Headers.Add("X-Pagination", vehicles?.CreateMetaData());
        var responseVehicles = vehicles?.Select(v => new VehicleDTO(v)).ToList();
        return Ok(responseVehicles);
    }

    [HttpGet("{id:int:min(1)}", Name="obter-veiculo")]
    [Authorize]
    public ActionResult<VehicleDTO> GetVehicle(int id) {
        var vehicle = _unitDB.VehicleRepository?.Get(v => v.Id == id);
        if (vehicle is null) {
            return NotFound();
        }
        return Ok(new VehicleDTO(vehicle));
    }

    [HttpPost]
    [Authorize]
    public ActionResult<VehicleDTO> PostVehicle([FromBody] VehicleDTO v) {
        if (v is null) {
            return BadRequest();
        }
        var newVehicle = _unitDB.VehicleRepository?.Add(new VehicleDB(v));
        _unitDB.Commit();
        var responseVehicle = new VehicleDTO(newVehicle);
        return new CreatedAtRouteResult("obter-veiculo",
            new {id = responseVehicle.Id}, responseVehicle);
    }

    [HttpPut("{id:int:min(1)}")]
    [Authorize]
    public ActionResult<VehicleDTO> PutVehicle(int id, [FromBody] VehicleDTO v) {
        if (v is null) {
            return BadRequest();
        }
        var vehicle = _unitDB.VehicleRepository?.Update(new VehicleDB(v));
        _unitDB.Commit();
        return Ok(new VehicleDTO(vehicle));
    }

    [HttpDelete("{id:int:min(1)}")]
    [Authorize(Policy = "AdminOnly")]
    public ActionResult<VehicleDTO> DeleteVehicle(int id) {
        var vehicleToDel = _unitDB.VehicleRepository?.Get(v => v.Id == id);
        if (vehicleToDel is null) {
            return BadRequest();
        }
        var vehicle = _unitDB.VehicleRepository?.Delete(vehicleToDel);
        _unitDB.Commit();
        return Ok(new VehicleDTO(vehicle));
    }
}