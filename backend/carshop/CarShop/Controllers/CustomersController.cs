using Microsoft.AspNetCore.Mvc;
using CarShop.Models;
using CarShop.Repositories;
using CarShop.DTO;

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
    public ActionResult<IEnumerable<CustomerDTO>> GetCustomers() {
        var customers = _unitDB.CustomerRepository.GetAll();
        var ResponseCustomers = customers.Select(c => new CustomerDTO(c)).ToList();
        return Ok(ResponseCustomers);
    }

    [HttpGet("{id:int:min(1)}", Name="obter-cliente")]
    public ActionResult<CustomerDTO> GetCustomer(int id) {
        var customer = _unitDB.CustomerRepository.Get(c => c.Id == id);
        if (customer is null) {
            return NotFound();
        }
        return Ok(new CustomerDTO(customer));
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
    public ActionResult<CustomerDTO> PostCustomer([FromBody] CustomerDTO requestCustomer) {
        if (requestCustomer is null) {
            return BadRequest();
        }
        var newCustomer = _unitDB.CustomerRepository.Add(new CustomerDB(requestCustomer));
        var responseCustomer = new CustomerDTO(newCustomer);
        _unitDB.Commit();
        return new CreatedAtRouteResult("obter-cliente", new { id = responseCustomer.Id}, responseCustomer);
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
    public ActionResult<CustomerDTO> Put(int id, [FromBody] CustomerDTO requestCustomer) {
        if (requestCustomer is null) {
            return BadRequest();
        }
        var updateCustomer = _unitDB.CustomerRepository.Update(new CustomerDB(requestCustomer));
        var responseCustomer = new CustomerDTO(updateCustomer);
        _unitDB.Commit();
        return Ok(responseCustomer);
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
    public ActionResult<CustomerDTO> Delete(int id) {
        var customerToDelete = _unitDB.CustomerRepository.Get(c => c.Id == id);
        if (customerToDelete is null) {
            return NotFound();
        }
        var deletedCustomer = _unitDB.CustomerRepository.Delete(customerToDelete);
        var responseCustomer = new CustomerDTO(deletedCustomer);
        _unitDB.Commit();
        return Ok(responseCustomer);
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