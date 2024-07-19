using Microsoft.AspNetCore.Mvc;
using CarShop.Models;
using CarShop.Repositories;
using CarShop.DTO;
using CarShop.HandlerQueryStrings;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult<IEnumerable<TransactionResponseDTO>> GetTransactionsValues([FromQuery] TransactionQueryFilter filter) {
        var transactions = _unitDB.TransactionRepository?.GetTransactionsWithFilter(filter);
        Response.Headers.Append("X-Pagination", transactions?.CreateMetaData());
        var responseTransactions = transactions?.Select(t => new TransactionResponseDTO(t)).ToList();
        return Ok(responseTransactions);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult<IEnumerable<TransactionDTO>> GetTransactions([FromQuery] TransactionQueryFilter filter) {
        var transactions = _unitDB.TransactionRepository?.GetTransactionsWithFilter(filter);
        Response.Headers.Append("X-Pagination", transactions?.CreateMetaData());
        var responseTransactions = transactions?.Select(t => new TransactionDTO(t)).ToList();
        return Ok(responseTransactions);
    }

    [HttpGet("{id:int:min(1)}", Name="nova-transacao")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult<TransactionDTO>    GetTransaction(int id) {
        var transaction = _unitDB.TransactionRepository?.Get(v => v.Id == id);
        if (transaction is null) {
            return NotFound();
        }
        return Ok(new TransactionDTO(transaction));
    }

    [HttpPost("compra")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult<TransactionDTO> PostTransactionBuy(
        [FromBody] TransactionDTO mov) {
        if (mov is null || mov.Vehicle is null) {
            return BadRequest();
        }
        mov.Type = "COMPRA";
        mov.Value = -(Math.Abs(mov.Value));
        var newTransaction = _unitDB.TransactionRepository?.Add(new FinancialTransactionsDB(mov));
        _unitDB.Commit();
        var responseTransaction = new TransactionDTO(newTransaction);
        return new CreatedAtRouteResult("nova-transacao", 
            new {id = responseTransaction.Id}, responseTransaction);
    }

    [HttpPost("venda")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult<TransactionDTO> PostTransactionSell(
        [FromBody] TransactionDTO mov) {
        if (mov is null || mov.Vehicle is null) {
            return BadRequest();
        }
        var vehicleOld = _unitDB.VehicleRepository?.Get(v => v.Id == mov.VehicleId);
        if (vehicleOld is not null && vehicleOld.Id == mov.Vehicle.Id) {
            vehicleOld.Copy(mov.Vehicle);
            _unitDB.VehicleRepository?.Update(vehicleOld);
            mov.Vehicle = null;
        }
        mov.Type = "VENDA";
        mov.Value = (Math.Abs(mov.Value));
        var newTransaction = _unitDB.TransactionRepository?.Add(new FinancialTransactionsDB(mov));
        _unitDB.Commit();
        var responseTransaction = new TransactionDTO(newTransaction);
        return new CreatedAtRouteResult("nova-transacao", 
            new {id = responseTransaction.Id}, responseTransaction);
    }

    [HttpPut("{id:int:min(1)}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult<TransactionDTO> PutTrnsaction([FromBody] TransactionDTO mov) {
        if (mov is null) {
            return BadRequest();
        }
        var transaction = _unitDB.TransactionRepository?.Update(new FinancialTransactionsDB(mov));
        _unitDB.Commit();
        return Ok(new TransactionDTO(transaction));
    }

    [Authorize(Policy = "AdminOnly", AuthenticationSchemes = "Bearer")]
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