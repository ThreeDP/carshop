using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CarShop.Context;
using CarShop.Models;
using CarShop.DTO;
using CarShop.Filters;
using System.Runtime.Intrinsics;

namespace CarShop.Controllers;

[ApiController]
[Route("movimentacoes")]
public class FinancialTransationsController : ControllerBase
{
    private readonly CarShopDataContext _ctx;
    private readonly ILogger<FinancialTransationsController> _logger;

    public FinancialTransationsController(CarShopDataContext context, ILogger<FinancialTransationsController> logger) {
        _ctx = context;
        _logger = logger;
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
    public ActionResult Post([FromBody] TransactionRequestDTO mov) {
        _logger.LogInformation($"Get on /movimentacoes with params [ {mov} ]");
        if (!ModelState.IsValid || mov is null)
            return BadRequest(ModelState);
        var v = _ctx.Vehicles?.FirstOrDefault( v => v.Id == mov.VehicleId);
        if (v is not null) {
            v.Copy(mov.Vehicle);
            _ctx.Entry(v).State = EntityState.Modified;
            mov.Vehicle = null;
        }
        var res = new FinancialTransactionsDB(mov);
        _ctx.FinancialTransactions?.Add(res);
        _ctx.SaveChanges();
        return new CreatedAtRouteResult("new-transation",
           new { id = res.Id }, res);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult Put(int id, [FromBody] FinancialTransactionsDB mov) {
        if (mov is null || id != mov.Id || !ModelState.IsValid) {
            return BadRequest();
        }
        _ctx.Entry(mov).State = EntityState.Modified;
        _ctx.SaveChanges();
        return Ok(mov);
    }
}

// {
//   "value": 11,
//   "Type": "string",
//   "customer_id": 1,
//   "VehicleId": 1,
//   "vehicle": {
//     "vehicle_id": 1,
//     "renavan": "string",
//     "license_plate": "string",
//     "brand": "string",
//     "model": "string",
//     "model_year": "2024-06-24T19:27:58.615Z",
//     "vehicle_type": "string",
//     "year_manufacture": "2024-06-24T19:27:58.615Z",
//     "description": "string",
//     "situation": "INDISPONIVEL"
//   }
// }