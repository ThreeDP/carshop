using Microsoft.AspNetCore.Mvc;
using CarShop.Models;
using CarShop.Repositories;
using CarShop.DTO;
using CarShop.HandlerQueryStrings;
using CarShop.Filters;
using Microsoft.AspNetCore.Authorization;

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

    [HttpGet]
    [ServiceFilter(typeof(CarShopLoggingFilter))]
    public ActionResult<IEnumerable<CustomerDTO>> GetCustomers([FromQuery] CustomerQueryFilter filter) {
        _logger.LogInformation($"Get on /clientes with params [{filter}]");
        var customers = _unitDB.CustomerRepository?.GetCustomersWithFilter(filter);
        Response.Headers.Append("X-Pagination", customers?.CreateMetaData());
        var ResponseCustomers = customers?.Select(c => new CustomerDTO(c)).ToList();
        return Ok(ResponseCustomers);
    }

    [Authorize]
    [HttpGet("{id:int:min(1)}", Name="obter-cliente")]
    public ActionResult<CustomerDTO> GetCustomer(int id) {
        var customer = _unitDB.CustomerRepository?.Get(c => c.Id == id);
        if (customer is null) {
            return NotFound();
        }
        return Ok(new CustomerDTO(customer));
    }

    [HttpPost]
    public ActionResult<CustomerDTO> PostCustomer([FromBody] CustomerDTO requestCustomer) {
        if (requestCustomer is null) {
            return BadRequest();
        }
        var newCustomer = _unitDB.CustomerRepository?.Add(new CustomerDB(requestCustomer));
        _unitDB.Commit();
        var responseCustomer = new CustomerDTO(newCustomer);
        return new CreatedAtRouteResult("obter-cliente", new { id = responseCustomer.Id}, responseCustomer);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<CustomerDTO> Put(int id, [FromBody] CustomerDTO requestCustomer) {
        if (requestCustomer is null) {
            return BadRequest();
        }
        var updateCustomer = _unitDB.CustomerRepository?.Update(new CustomerDB(requestCustomer));
        _unitDB.Commit();
        var responseCustomer = new CustomerDTO(updateCustomer);
        return Ok(responseCustomer);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CustomerDTO> Delete(int id) {
        var customerToDelete = _unitDB.CustomerRepository?.Get(c => c.Id == id);
        if (customerToDelete is null) {
            return NotFound();
        }
        var deletedCustomer = _unitDB.CustomerRepository?.Delete(customerToDelete);
        _unitDB.Commit();
        var responseCustomer = new CustomerDTO(deletedCustomer);
        return Ok(responseCustomer);
    }
}