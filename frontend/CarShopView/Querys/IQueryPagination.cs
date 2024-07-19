namespace CarShopView.Querys;

public interface IQueryPagination {
    public int?     PageNumber { get; set; }
    public int?     PageSize { get; set; }
}
