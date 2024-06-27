using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarShop.Context;
using CarShop.Models;
using CarShop.Filters;
using CarShop.Repositories;

namespace CarShop.Controllers;

[ApiController]
[Route("clientes")]
public class CustomersController : ControllerBase
{
    private readonly IUnitOfWork    _unitDB;
    private readonly ILogger<CustomersController> _logger;

    public CustomersController(IUnitOfWork uow, ILogger<CustomersController> logger) {
        _unitDB = uow;
        _logger = logger;
    }

    // [HttpGet()]
    // [ServiceFilter(typeof(CarShopLoggingFilter))]
    // public async Task<ActionResult<IEnumerable<CustomerDB>>> GetAsync(string? name, string? docType, int range=10)
    // {
    //     _logger.LogInformation($"Get on /clientes with params [{name} {docType} {range}]");
    //     var filterName = name?.ToUpper().Normalize();
    //     var filterDocType = docType?.ToUpper();
    //     var customer = _ctx.Customers?.OrderBy(c => c.Name).AsQueryable();

    //     if (filterDocType is not null) {
    //         customer = customer.Where(c => c.DocType == filterDocType);
    //     }

    //     if (filterName is not null) {
    //         customer = customer.Where(c => c.Name.ToUpper().StartsWith(filterName));
    //     }

    //     var res = await customer.Take(range)
    //         .ToArrayAsync();

    //     if (res is null)
    //         return NotFound();
    //     return Ok(res);
    // }

    [HttpGet]
    public ActionResult<IEnumerable<CustomerDB>> GetCustomers() {
        var customers = _unitDB.CustomerRepository.GetAll();
        return Ok(customers);
    }

    [HttpGet("{id:int:min(1)}", Name="obter-cliente")]
    public ActionResult<CustomerDB> GetCustomer(int id) {
        var customer = _unitDB.CustomerRepository.Get(c => c.Id == id);
        if (customer is null) {
            return NotFound();
        }
        return Ok(customer);
    }

    // [HttpGet("{id:int:min(1)}", Name="ObterCliente")]
    // [ServiceFilter(typeof(CarShopLoggingFilter))]
    // public async Task<ActionResult<CustomerDB>> GetAsync(int? id)
    // {
    //     var c = await _ctx.Customers?
    //         .AsNoTracking()
    //         .FirstOrDefaultAsync(c => c.Id == id);
    //     if (c is null) {
    //         return NotFound();
    //     }
    //     return Ok(c);
    // }

    [HttpPost]
    public ActionResult<CustomerDB> PostCustomer([FromBody] CustomerDB c) {
        if (c is null) {
            return BadRequest();
        }
        var customer = _unitDB.CustomerRepository.Add(c);
        _unitDB.Commit();
        return new CreatedAtRouteResult("obter-cliente", new { id = customer.Id}, customer);
    }


    // [HttpPost]
    // [ServiceFilter(typeof(CarShopLoggingFilter))]
    // public ActionResult Post([FromBody] CustomerDB c)
    // {
    //     if (!ModelState.IsValid || c is null)
    //         return BadRequest(ModelState);
    //     if (_ctx.Customers is null)
    //         return NotFound();
    //     _ctx.Customers?.Add(c);
    //     _ctx.SaveChanges();
    //     return new CreatedAtRouteResult("ObterCliente",
    //         new { id = c.Id }, c);
    // }

    [HttpPut("id:int:min(1)")]
    public ActionResult<CustomerDB> Put(int id, [FromBody] CustomerDB c) {
        if (c is null) {
            return BadRequest();
        }
        var customer = _unitDB.CustomerRepository.Update(c);
        _unitDB.Commit();
        return Ok(c);
    }

    // [HttpPut("{id:int:min(1)}")]
    // [ServiceFilter(typeof(CarShopLoggingFilter))]
    // public ActionResult Put(int id, [FromBody] CustomerDB c)
    // {
    //     if (c is null ||id != c.Id || !ModelState.IsValid) 
    //         return BadRequest();
    //     _ctx.Entry(c).State = EntityState.Modified;
    //     _ctx.SaveChanges();
    //     return Ok(c);
    // }

    [HttpDelete("{id:int}")]
    public ActionResult<CustomerDB> Delete(int id) {
        var customerToDelete = _unitDB.CustomerRepository.Get(c => c.Id == id);
        if (customerToDelete is null) {
            return NotFound();
        }
        var customer = _unitDB.CustomerRepository.Delete(customerToDelete);
        _unitDB.Commit();
        return Ok(customer);
    }

    // [HttpDelete("{id:int}")]
    // [ServiceFilter(typeof(CarShopLoggingFilter))]
    // public ActionResult Delete(int id)
    // {
    //     var customer = _ctx.Customers?.FirstOrDefault(c => c.Id == id);
    //     if (customer is null) { return NotFound("Cliente não encontrado!"); };
    //     _ctx.Customers?.Remove(customer);
    //     _ctx.SaveChanges();
    //     return Ok(customer);
    // }
}