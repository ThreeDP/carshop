using CarShop.HandlerQueryStrings;
using CarShop.Models;
using HandlerQueryStrings;

namespace CarShop.Repositories;

public interface ITransactionRepository : IRepository<FinancialTransactionsDB> {
    public PagedList<FinancialTransactionsDB> GetTransactionsWithFilter(TransactionQueryFilter filter);
}