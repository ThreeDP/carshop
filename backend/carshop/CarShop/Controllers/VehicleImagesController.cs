using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarShop.Context;
using CarShop.Models;
using CarShop.Filters;

namespace CarShop.Controllers;

[ApiController]
[Route("vehicles/images")]
public class VehicleImagesController : ControllerBase
{
    private readonly CarShopDataContext _ctx;

    public VehicleImagesController(CarShopDataContext ctx) {
        _ctx = ctx;
    }

    [HttpGet]
    public ActionResult<IEnumerable<VehicleImageDB>> Get() {
        var imgs = _ctx.VehicleImages.AsNoTracking().Take(5).ToList();
        if (imgs is null) {
            return NotFound();
        }
        return imgs;
    }

    [HttpPost]
    public ActionResult Post([FromBody] VehicleImageDB image) {
        if (!ModelState.IsValid || image is null) {
            return BadRequest(ModelState);
        }
        _ctx.VehicleImages.Add(image);
        _ctx.SaveChanges();
        return Ok();
    }
}