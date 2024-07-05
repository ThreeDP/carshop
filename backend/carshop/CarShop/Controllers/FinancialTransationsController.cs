using Microsoft.AspNetCore.Mvc;
using CarShop.Models;
using CarShop.Repositories;
using CarShop.DTO;
using CarShop.HandlerQueryStrings;

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

    [HttpGet("valores")]
    public ActionResult<IEnumerable<TransactionResponseDTO>> GetTransactionsValues([FromQuery] TransactionQueryFilter filter) {
        var transactions = _unitDB.TransactionRepository?.GetTransactionsWithFilter(filter);
        Response.Headers.Append("X-Pagination", transactions?.CreateMetaData());
        var responseTransactions = transactions?.Select(t => new TransactionResponseDTO(t)).ToList();
        return Ok(responseTransactions);
    }

    [HttpGet]
    public ActionResult<IEnumerable<TransactionDTO>> GetTransactions([FromQuery] TransactionQueryFilter filter) {
        var transactions = _unitDB.TransactionRepository?.GetTransactionsWithFilter(filter);
        Response.Headers.Append("X-Pagination", transactions?.CreateMetaData());
        var responseTransactions = transactions?.Select(t => new TransactionDTO(t)).ToList();
        return Ok(responseTransactions);
    }

    [HttpGet("{id:int:min(1)}", Name="nova-transacao")]
    public ActionResult<TransactionDTO>    GetTransaction(int id) {
        var transaction = _unitDB.TransactionRepository?.Get(v => v.Id == id);
        if (transaction is null) {
            return NotFound();
        }
        return Ok(new TransactionDTO(transaction));
    }

    [HttpPost]
    public ActionResult<TransactionDTO> PostTransaction(
        [FromBody] TransactionDTO mov) {
        if (mov is null) {
            return BadRequest();
        }
        // if (mov.Vehicle is not null) {
        //      _unitDB.VehicleRepository?.Update(new VehicleDB(mov.Vehicle));
        //      mov.Vehicle = null;
        // }
        mov.Value = Math.Abs(mov.Value);
        if (mov.Type == "COMPRA") {
            mov.Value = -(mov.Value);
        }
        var newTransaction = _unitDB.TransactionRepository?.Add(new FinancialTransactionsDB(mov));
        _unitDB.Commit();
        var responseTransaction = new TransactionDTO(newTransaction);
        return new CreatedAtRouteResult("nova-transacao", 
            new {id = responseTransaction.Id}, responseTransaction);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<TransactionDTO> PutTrnsaction([FromBody] TransactionDTO mov) {
        if (mov is null) {
            return BadRequest();
        }
        var transaction = _unitDB.TransactionRepository?.Update(new FinancialTransactionsDB(mov));
        _unitDB.Commit();
        return Ok(new TransactionDTO(transaction));
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<TransactionDTO> DeleteTransaction(int id) {
        var transactionToDel = _unitDB.TransactionRepository?.Get(t => t.Id == id);
        if (transactionToDel is null) {
            return NotFound();
        }
        var mov = _unitDB.TransactionRepository?.Delete(transactionToDel);
        _unitDB.Commit();
        return Ok(new TransactionDTO(mov));
    }
}