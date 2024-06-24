using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarShop.Context;
using CarShop.Models;
using CarShop.Filters;

namespace CarShop.Controllers;

[ApiController]
[Route("movimentacoes")]
public class FinancialTransationsController : ControllerBase
{
    private readonly CarShopDataContext _ctx;

    public FinancialTransationsController(CarShopDataContext context) {
        _ctx = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FinancialTransactionsDB>>> GetAsync(string? customer, string? vehicle, string? licensePlate, int range=10)
    {
        var mov = await _ctx.FinancialTransactions?
            .AsNoTracking()
            .Include(f => f.Customer)
            .Include(f => f.Vehicle)
            .Take(range)
            .ToListAsync();
        if (mov is null) {
            return NotFound();
        }
        return Ok(mov);
    }

    [HttpGet("{id:int:min(1)}", Name="new-transation")]
    public ActionResult<FinancialTransactionsDB> GetAsync(int id) {
        var mov = _ctx.FinancialTransactions?
            .AsNoTracking()
            .Include(f => f.Customer)
            .Include(f => f.Vehicle)
            .FirstOrDefault(t => t.Id == id);
        if (mov is null) {
            return NotFound();
        }
        return mov;
    }

    [HttpPost]
    public ActionResult Post([FromBody] FinancialTransactionsDB mov) {
        if (!ModelState.IsValid || mov is null)
            return BadRequest(ModelState);
        _ctx.FinancialTransactions.Add(mov);
        _ctx.SaveChanges();
        return new CreatedAtRouteResult("new-transation",
            new { id = mov.Id }, mov);
    }
}