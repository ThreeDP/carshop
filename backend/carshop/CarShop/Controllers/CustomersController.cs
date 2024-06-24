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
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(CarShopDataContext ctx, ILogger<CustomersController> logger) {
        _ctx = ctx;
        _logger = logger;
    }

    [HttpGet()]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public async Task<ActionResult<IEnumerable<CustomerDB>>> GetAsync(string? name, string? docType, int range=10)
    {
        _logger.LogInformation($"Get on /clientes with params [{name} {docType} {range}]");
        var filterName = name?.ToUpper().Normalize();
        var filterDocType = docType?.ToUpper();
        var c = await _ctx.Customers?.AsNoTracking()
            .Where(
                c => string.IsNullOrEmpty(filterDocType)
                || c.DocType == filterDocType
            )
            .Where(
                c => string.IsNullOrEmpty(filterName)
                || c.Name.ToUpper()
                    .StartsWith(filterName)
            )
            .OrderBy(c => c.Name)
            .Take(range)
            .ToArrayAsync();
        if (c is null)
            return NotFound();
        return Ok(c);
    }

    [HttpGet("{id:int:min(1)}", Name="ObterCliente")]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public async Task<ActionResult<CustomerDB>> GetAsync(int? id)
    {
        var c = await _ctx.Customers?
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        if (c is null) {
            return NotFound();
        }
        return Ok(c);
    }

    [HttpPost]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public ActionResult Post([FromBody] CustomerDB c)
    {
        if (!ModelState.IsValid || c is null)
            return BadRequest(ModelState);
        if (_ctx.Customers is null)
            return NotFound();
        _ctx.Customers?.Add(c);
        _ctx.SaveChanges();
        return new CreatedAtRouteResult("ObterCliente",
            new { id = c.Id }, c);
    }

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public ActionResult Put(int id, [FromBody] CustomerDB c)
    {
        if (id != c.Id || !ModelState.IsValid || c is null) 
            return BadRequest();
        _ctx.Entry(c).State = EntityState.Modified;
        _ctx.SaveChanges();
        return Ok(c);
    }

    [HttpDelete("{id:int}")]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public ActionResult Delete(int id)
    {
        var customer = _ctx.Customers?.FirstOrDefault(c => c.Id == id);
        if (customer is null) { return NotFound("Cliente não encontrado!"); };
        _ctx.Customers?.Remove(customer);
        _ctx.SaveChanges();
        return Ok(customer);
    }
}