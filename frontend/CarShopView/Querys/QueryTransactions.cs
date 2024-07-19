namespace CarShopView.Querys;

public class QueryTransactions : QueryPagination, IQueryTransactions {
    public string? Type { get; set; }
    public double? MinValue { get; set; }
    public double? MaxValue { get; set; }
}