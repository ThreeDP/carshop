using Microsoft.AspNetCore.Mvc;
using CarShop.Models;
using CarShop.Repositories;
using CarShop.DTO;

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
    public ActionResult<IEnumerable<VehicleImageDTO>> GetImages() {
        var images = _unitDB.VehicleImageRepository.GetAll();
        var responseImages = images.Select(i => new VehicleImageDTO(i)).ToList();
        return Ok(responseImages);
    }

    [HttpGet("{id:int:min(1)}", Name="obter-imagem-veiculo")]
    public ActionResult<VehicleImageDTO> GetImage(int id) {
        var image = _unitDB.VehicleImageRepository.Get( i => i.VehicleImageDBId == id);
        if (image is null) {
            return NotFound();
        }
        return Ok(new VehicleImageDTO(image));
    }

    [HttpPost]
    public ActionResult<VehicleImageDTO> PostImage([FromBody] VehicleImageDTO img) {
        if (img is null) {
            return BadRequest();
        }
        var newImage = _unitDB.VehicleImageRepository.Add(new VehicleImageDB(img));
        var responseImage = new VehicleImageDTO(newImage);
        _unitDB.Commit();
        return new CreatedAtRouteResult("obter-imagem-veiculo",
            new {id = responseImage.Id}, responseImage);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<VehicleImageDTO> PutImage(int id, [FromBody] VehicleImageDTO img) {
        if (img is null) {
            return BadRequest();
        }
        var updatedImage = _unitDB.VehicleImageRepository.Update(new VehicleImageDB(img));
        _unitDB.Commit();
        return Ok(new VehicleImageDTO(updatedImage));
    }

    [HttpDelete("id:int:min(1)")]
    public ActionResult<VehicleImageDTO> DeleteImage(int id) {
        var image = _unitDB.VehicleImageRepository.Get(i => i.VehicleImageDBId == id);
        if (image is null) {
            return NotFound();
        }
        var deletedImage = _unitDB.VehicleImageRepository.Delete(image);
        _unitDB.Commit();
        return Ok(new VehicleImageDTO(deletedImage));
    }

}