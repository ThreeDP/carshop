namespace CarShopView.Querys;

public interface IQueryTransactions : IQueryPagination{
    public string? Type { get; set; }
    public double? MinValue { get; set; }
    public double? MaxValue { get; set; }
}