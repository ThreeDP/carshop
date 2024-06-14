using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarShop.Context;
using CarShop.Models;

namespace CarShop.Controllers;

[ApiController]
[Route("clientes")]
public class ClientsController : ControllerBase
{
    private readonly CarShopDataContext _ctx;

    public ClientsController(CarShopDataContext ctx) {
        _ctx = ctx;
    }

    [HttpGet()]
    public ActionResult<IEnumerable<ClientDB>> Get()
    {
        var c = _ctx.Clients.AsNoTracking().Take(10).ToArray();
        if (c is null) {
            return NotFound();
        }
        return c;
    }

    [HttpGet("{id:int}", Name="ObterCliente")]
    public ActionResult<ClientDB> Get(int id)
    {
        var c = _ctx.Clients.AsNoTracking().FirstOrDefault(c => c.ClientDBId == id);
        if (c is null) {
            return NotFound();
        }
        return c;
    }

    [HttpPost]
    public ActionResult Post([FromBody] ClientDB c)
    {
        if (!ModelState.IsValid || c is null)
            return BadRequest(ModelState);
        _ctx.Clients.Add(c);
        _ctx.SaveChanges();
        return new CreatedAtRouteResult("ObterCliente",
            new { id = c.ClientDBId }, c);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, [FromBody] ClientDB c)
    {
        if (id != c.ClientDBId || ModelState.IsValid || c is null)
        { return BadRequest(); };

        _ctx.Entry(c).State = EntityState.Modified;
        _ctx.SaveChanges();

        return Ok(c);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var client = _ctx.Clients.FirstOrDefault(c => c.ClientDBId == id);

        if (client is null) { return NotFound("Cliente não encontrado!"); };
        _ctx.Clients.Remove(client);
        _ctx.SaveChanges();

        return Ok(client);
    }
}