using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarShop.Context;
using CarShop.Models;
using CarShop.Filters;

namespace CarShop.Controllers;

[ApiController]
[Route("clientes")]
public class CustomersController : ControllerBase
{
    private readonly CarShopDataContext _ctx;

    public CustomersController(CarShopDataContext ctx) {
        _ctx = ctx;
    }

    [HttpGet()]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public async Task<ActionResult<IEnumerable<CustomerDB>>> Get()
    {
        try {
            var c = await _ctx.Customers.AsNoTracking().OrderBy(c => c.Name).Take(10).ToArrayAsync();
            return Ok(c);
        } catch {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Erro ao processar sua solicitação.");
        }
    }

    [HttpGet("{id:int:min(1)}", Name="ObterCliente")]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public ActionResult<CustomerDB> Get(int id)
    {
        try {
            var c = _ctx.Customers.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (c is null) {
                return NotFound();
            }
            return Ok(c);
        } catch {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao processar sua solicitação para o id: {id}.");
        }
    }

    [HttpGet("{name}")]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public ActionResult<IEnumerable<CustomerDB>> Get(string name) {
        try {
            var c = _ctx.Customers.AsNoTracking()
                .Where( c => c.Name.ToUpper()
                    .StartsWith(name.ToUpper())
                )
                .Take(10)
                .ToList();
            if (c is null || c.Count() == 0) {
                return NotFound();
            }
            return Ok(c);
        } catch {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao encontrar customere {name}");
        }
    }

    [HttpPost]
    public ActionResult Post([FromBody] CustomerDB c)
    {
        try {
            if (!ModelState.IsValid || c is null)
                return BadRequest(ModelState);
            _ctx.Customers.Add(c);
            _ctx.SaveChanges();
            return new CreatedAtRouteResult("ObterCustomere",
                new { id = c.Id }, c);
        } catch {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao cadastrar customere {c}. Tente mais tarde...");
        }
    }

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public ActionResult Put(int id, [FromBody] CustomerDB c)
    {
        try {
            if (id != c.Id || !ModelState.IsValid || c is null) 
                return BadRequest();
            _ctx.Entry(c).State = EntityState.Modified;
            _ctx.SaveChanges();
            return Ok(c);
        } catch {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao atualizar customere {c}. Tente mais tarde...");
        }
    }

    [HttpDelete("{id:int}")]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public ActionResult Delete(int id)
    {
        try {
            var customer = _ctx.Customers.FirstOrDefault(c => c.Id == id);
            if (customer is null) { return NotFound("Customere não encontrado!"); };
            _ctx.Customers.Remove(customer);
            _ctx.SaveChanges();
            return Ok(customer);
        } catch {
            return StatusCode(StatusCodes.Status500InternalServerError,
                $"Erro ao deletar customere with id: {id}. Tente mas tarde...");
        }
    }
}