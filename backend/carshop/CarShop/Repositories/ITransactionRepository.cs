using CarShop.HandlerQueryStrings;
using CarShop.Models;

namespace CarShop.Repositories;

public interface ITransactionRepository : IRepository<FinancialTransactionsDB> {
    public IEnumerable<FinancialTransactionsDB> GetTransactionsWithFilter(TransactionQueryFilter filter);
}