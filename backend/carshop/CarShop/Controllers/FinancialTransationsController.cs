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

    [HttpGet]
    public ActionResult<IEnumerable<TransactionResponseDTO>> GetTransactions([FromQuery] TransactionQueryFilter filter) {
        var transactions = _unitDB.TransactionRepository.GetTransactionsWithFilter(filter);
        Response.Headers.Append("X-Pagination", transactions.CreateMetaData());
        var responseTransactions = transactions.Select(t => new TransactionResponseDTO(t)).ToList();
        return Ok(responseTransactions);
    }

    [HttpGet("{id:int:min(1)}", Name="nova-transacao")]
    public ActionResult<TransactionResponseDTO>    GetTransaction(int id) {
        var transaction = _unitDB.TransactionRepository.Get(v => v.Id == id);
        if (transaction is null) {
            return NotFound();
        }
        return Ok(new TransactionResponseDTO(transaction));
    }

    [HttpPost]
    public ActionResult<TransactionResponseDTO> PostTransaction(
        [FromBody] TransactionDTO mov) {
        if (mov is null) {
            return BadRequest();
        }
        if (mov.Vehicle is not null) {
             _unitDB.VehicleRepository.Update(new VehicleDB(mov.Vehicle));
             mov.Vehicle = null;
        }
        var newTransaction = _unitDB.TransactionRepository.Add(new FinancialTransactionsDB(mov));
        var responseTransaction = new TransactionResponseDTO(newTransaction);
        _unitDB.Commit();
        return new CreatedAtRouteResult("nova-transacao", 
            new {id = responseTransaction.Id}, responseTransaction);
    }

    [HttpPut("{id:int:min(1)}")]
    public ActionResult<TransactionResponseDTO> PutTrnsaction([FromBody] TransactionDTO mov) {
        if (mov is null) {
            return BadRequest();
        }
        var transaction = _unitDB.TransactionRepository.Update(new FinancialTransactionsDB(mov));
        _unitDB.Commit();
        return Ok(new TransactionResponseDTO(transaction));
    }

    [HttpDelete("{id:int:min(1)}")]
    public ActionResult<TransactionResponseDTO> DeleteTransaction(int id) {
        var transactionToDel = _unitDB.TransactionRepository.Get(t => t.Id == id);
        if (transactionToDel is null) {
            return NotFound();
        }
        var mov = _unitDB.TransactionRepository.Delete(transactionToDel);
        return Ok(new TransactionResponseDTO(mov));
    }
}