using CarShop.Context;
using CarShop.HandlerQueryStrings;
using Microsoft.EntityFrameworkCore;
using CarShop.Models;
using Microsoft.AspNetCore.Mvc;
using HandlerQueryStrings;

namespace CarShop.Repositories;

public class TransactionRepository : Repository<FinancialTransactionsDB>, ITransactionRepository {
    public TransactionRepository(CarShopDataContext context) : base(context) {

    }
    public PagedList<FinancialTransactionsDB> GetTransactionsWithFilter(TransactionQueryFilter filter) {
        var transactions = _ctx.FinancialTransactions?
                            .Include(t => t.Customer)
                            .Include(t => t.Vehicle)
                            .AsQueryable();
        transactions = transactions.Where(t => t.Value > filter.MinValue);
        if (filter.MaxValue is not null) {
            transactions = transactions.Where(t => t.Value <= filter.MaxValue);
        }
        return PagedList<FinancialTransactionsDB>.ToPagedList(transactions, filter.PageNumber, filter.PageSize);
    }
}