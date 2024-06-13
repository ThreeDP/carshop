using Microsoft.AspNetCore.Mvc;
using CarShop.Context;
using CarShop.Models;

namespace CarShop.Controllers;

[ApiController]
[Route("Clientes")]
public class ClientsController : ControllerBase
{
    private readonly CarShopDataContext _ctx;

    public ClientsController(CarShopDataContext ctx) {
        _ctx = ctx;
    }

    [HttpGet()]
    public ActionResult<IEnumerable<ClientDB>> Get()
    {
        var c = _ctx.Clients.ToArray();
        if (c == null) {
            return NotFound();
        }
        return c;
    }
    // public IEnumerable<string> Get()
    // {
    //     using (var db = new CarShopDataContext("Server=localhost;Port=5400;Database=CarShop;Uid=postgres;"))
    //     {
    //         return db.Clients.ToArray();
    //     }
    // }
}