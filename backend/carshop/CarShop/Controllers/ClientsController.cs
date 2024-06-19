using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarShop.Context;
using CarShop.Models;
using CarShop.Filters;

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
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public async Task<ActionResult<IEnumerable<ClientDB>>> Get()
    {
        try {
            var c = await _ctx.Clients.AsNoTracking().OrderBy(c => c.Name).Take(10).ToArrayAsync();
            if (c is null) {
                return NotFound();
            }
            return c;
        }
        catch {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Erro ao processar sua solicitação.");
        }
    }

    [HttpGet("{id:int:min(1)}", Name="ObterCliente")]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public ActionResult<ClientDB> Get(int id)
    {
        try {
            var c = _ctx.Clients.AsNoTracking().FirstOrDefault(c => c.ClientDBId == id);
            if (c is null) {
                return NotFound();
            }
            return c;
        } catch {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao processar sua solicitação para o id: {id}.");
        }
    }

    [HttpGet("{name}")]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public ActionResult<IEnumerable<ClientDB>> Get(string name) {
        try {
            var c = _ctx.Clients.AsNoTracking()
                .Where( c => c.Name.ToUpper()
                    .StartsWith(name.ToUpper())
                )
                .Take(10)
                .ToList();
            if (c is null) {
                return NotFound();
            }
            return c;
        } catch {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao encontrar cliente {name}");
        }
    }

    [HttpPost]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public ActionResult Post([FromBody] ClientDB c)
    {
        try {
            if (!ModelState.IsValid || c is null)
                return BadRequest(ModelState);
            _ctx.Clients.Add(c);
            _ctx.SaveChanges();
            return new CreatedAtRouteResult("ObterCliente",
                new { id = c.ClientDBId }, c);
        } catch {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao cadastrar cliente {c}. Tente mais tarde...");
        }
    }

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public ActionResult Put(int id, [FromBody] ClientDB c)
    {
        try {
            if (id != c.ClientDBId || !ModelState.IsValid || c is null) 
                return BadRequest();
            _ctx.Entry(c).State = EntityState.Modified;
            _ctx.SaveChanges();
            return Ok(c);
        } catch {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao atualizar cliente {c}. Tente mais tarde...");
        }
    }

    [HttpDelete("{id:int}")]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public ActionResult Delete(int id)
    {
        try {
            var client = _ctx.Clients.FirstOrDefault(c => c.ClientDBId == id);
            if (client is null) { return NotFound("Cliente não encontrado!"); };
            _ctx.Clients.Remove(client);
            _ctx.SaveChanges();
            return Ok(client);
        } catch {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao deletar cliente with id: {id}. Tente mas tarde...");
        }
    }
}