using CarShop.Context;
using CarShop.HandlerQueryStrings;
using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.Repositories;

public class TransactionRepository : Repository<FinancialTransactionsDB>, ITransactionRepository {
    public TransactionRepository(CarShopDataContext context) : base(context) {

    }
    public IEnumerable<FinancialTransactionsDB> GetTransactionsWithFilter(TransactionQueryFilter? filter) {
        var transaction = _ctx.FinancialTransactions?
                            .Include(t => t.Customer)
                            .Include(t => t.Vehicle)
                            .AsQueryable();
        transaction = transaction.Where(t => t.Value > filter.MinValue);
        if (filter.MaxValue is not null)
            transaction = transaction.Where(t => t.Value <= filter.MaxValue);
        transaction = transaction.Skip((filter.PageNumber - 1) * filter.PageSize);
        transaction = transaction.Take(filter.PageSize);
        return transaction.ToList();
    }
}