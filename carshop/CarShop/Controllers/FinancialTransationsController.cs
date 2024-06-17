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
    public ActionResult<IEnumerable<FinancialTransactionsDB>> Get()
    {
        var mov = _ctx.FinancialTransactions.AsNoTracking().Take(10).ToList();
        if (mov is null) {
            return NotFound();
        }
        return mov;
    }

    [HttpGet("{id:int:min(1)}", Name="new-transation")]
    public ActionResult<FinancialTransactionsDB> Get(int id) {
        var mov = _ctx.FinancialTransactions.AsNoTracking().FirstOrDefault(t => t.FinancialTransactionDBId == id);
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
            new { id = mov.FinancialTransactionDBId }, mov);
    }
}