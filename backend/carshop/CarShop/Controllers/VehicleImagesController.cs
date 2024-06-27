using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarShop.Context;
using CarShop.Models;
using CarShop.Filters;
using CarShop.Repositories;

namespace CarShop.Controllers;

[ApiController]
[Route("veiculos/imagens")]
public class VehicleImagesController : ControllerBase
{
    private readonly IUnitOfWork _unitDB;

    public VehicleImagesController(IUnitOfWork uow) {
        _unitDB = uow;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VehicleImageDB>> GetImages() {
        var images = _unitDB.VehicleImageRepository.GetAll();
        return Ok(images);
    }

    [HttpGet("{id:int:min(1)}", Name="obter-imagem-veiculo")]
    public ActionResult<VehicleImageDB> GetImage(int id) {
        var image = _unitDB.VehicleImageRepository.Get( i => i.VehicleImageDBId == id);
        if (image is null) {
            return NotFound();
        }
        return Ok(image);
    }

    [HttpPost]
    public ActionResult<VehicleImageDB> PostImage([FromBody] VehicleImageDB img) {
        if (img is null) {
            return BadRequest();
        }
        var image = _unitDB.VehicleImageRepository.Add(img);
        _unitDB.Commit();
        return new CreatedAtRouteResult("obter-imagem-veiculo",
            new {id = image.VehicleImageDBId}, image);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<VehicleImageDB> PutImage(int id, [FromBody] VehicleImageDB img) {
        if (img is null) {
            return BadRequest();
        }
        var image = _unitDB.VehicleImageRepository.Update(img);
        _unitDB.Commit();
        return Ok(image);
    }

    [HttpDelete("id:int:min(1)")]
    public ActionResult<VehicleImageDB> DeleteImage(int id) {
        var image = _unitDB.VehicleImageRepository.Get(i => i.VehicleImageDBId == id);
        if (image is null) {
            return NotFound();
        }
        _unitDB.VehicleImageRepository.Delete(image);
        _unitDB.Commit();
        return Ok(image);
    }

}