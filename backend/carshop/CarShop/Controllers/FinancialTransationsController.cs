using Microsoft.AspNetCore.Mvc;
using CarShop.Models;
using CarShop.Repositories;
using CarShop.DTO;

namespace CarShop.Controllers;

[ApiController]
[Route("movimentacoes")]
public class FinancialTransationsController : ControllerBase
{
    private IUnitOfWork                 _unitDB;
    private readonly ILogger<FinancialTransationsController> _logger;

    public FinancialTransationsController(IUnitOfWork uow, ILogger<FinancialTransationsController> logger) {
        _unitDB = uow;
        _logger = logger;
    }

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<FinancialTransactionsDB>>> GetAsync(string? customer, string? vehicle, string? licensePlate, int range=10)
    // {
    //     var mov = await _ctx.FinancialTransactions?
    //         .AsNoTracking()
    //         .Include(f => f.Customer)
    //         .Include(f => f.Vehicle)
    //         .Take(range)
    //         .ToListAsync();
    //     if (mov is null) {
    //         return NotFound();
    //     }
    //     return Ok(mov);
    // }

    [HttpGet]
    public ActionResult<IEnumerable<TransactionDTO>> GetTransactions() {
        var transactions = _unitDB.TransactionRepository.GetAll();
        var responseTransactions = transactions.Select(t => new TransactionDTO(t)).ToList();
        return Ok(responseTransactions);
    }

    [HttpGet("{id:int:min(1)}", Name="nova-transacao")]
    public ActionResult<TransactionDTO>    GetTransaction(int id) {
        var transaction = _unitDB.TransactionRepository.Get(v => v.Id == id);
        if (transaction is null) {
            return NotFound();
        }
        return Ok(new TransactionDTO(transaction));
    }

    // [HttpGet("{id:int:min(1)}", Name="new-transation")]
    // public ActionResult<FinancialTransactionsDB> GetAsync(int id) {
    //     var mov = _ctx.FinancialTransactions?
    //         .AsNoTracking()
    //         .Include(f => f.Customer)
    //         .Include(f => f.Vehicle)
    //         .FirstOrDefault(t => t.Id == id);
    //     if (mov is null) {
    //         return NotFound();
    //     }
    //     return mov;
    // }

    [HttpPost]
    public ActionResult<TransactionDTO> PostTransaction(
        [FromBody] TransactionDTO mov) {
        if (mov is null) {
            return BadRequest();
        }
        var newTransaction = _unitDB.TransactionRepository.Add(new FinancialTransactionsDB(mov));
        var responseTransaction = new TransactionDTO(newTransaction);
        _unitDB.Commit();
        return new CreatedAtRouteResult("nova-transacao", 
            new {id = responseTransaction.Id}, responseTransaction);
    }

    // [HttpPost]
    // public ActionResult Post([FromBody] TransactionRequestDTO mov) {
    //     _logger.LogInformation($"Get on /movimentacoes with params [ {mov} ]");
    //     if (!ModelState.IsValid || mov is null)
    //         return BadRequest(ModelState);
    //     var v = _ctx.Vehicles?.FirstOrDefault( v => v.Id == mov.VehicleId);
    //     if (v is not null) {
    //         v.Copy(mov.Vehicle);
    //         _ctx.Entry(v).State = EntityState.Modified;
    //         mov.Vehicle = null;
    //     }
    //     var res = new FinancialTransactionsDB(mov);
    //     _ctx.FinancialTransactions?.Add(res);
    //     _ctx.SaveChanges();
    //     return new CreatedAtRouteResult("new-transation",
    //        new { id = res.Id }, res);
    // }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<TransactionDTO> PutTrnsaction([FromBody] TransactionDTO mov) {
        if (mov is null) {
            return BadRequest();
        }
        var transaction = _unitDB.TransactionRepository.Update(new FinancialTransactionsDB(mov));
        _unitDB.Commit();
        return Ok(new TransactionDTO(transaction));
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<TransactionDTO> DeleteTransaction(int id) {
        var transactionToDel = _unitDB.TransactionRepository.Get(t => t.Id == id);
        if (transactionToDel is null) {
            return NotFound();
        }
        var mov = _unitDB.TransactionRepository.Delete(transactionToDel);
        return Ok(new TransactionDTO(mov));
    }
}