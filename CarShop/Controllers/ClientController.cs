using Microsoft.AspNetCore.Mvc;

namespace CarShop.Controllers;

[ApiController]
[Route("Clientes")]
public class ClientController : ControllerBase
{
    [HttpGet()]
    public IEnumerable<Client> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Client {
            Name = "Carlos",
            DocType = "RG",
            DocNumber = "165.352.985-58",
            Tel = "(11) 98654-3654",
            Active = true
        })
        .ToArray();
    }
}