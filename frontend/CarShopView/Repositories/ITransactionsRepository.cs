using System.Threading.Tasks;
using Refit;
using CarShopView.Models;
using CarShopView.Querys;

namespace CarShopView.Repositories;

public interface ITransactionsRepository {
    [Get("/movimentacoes")]
    Task<ApiResponse<IEnumerable<ITransactionResponse>>> GetTransactions(IQueryTransactions filter, [Authorize("Bearer")] string authorization);
}