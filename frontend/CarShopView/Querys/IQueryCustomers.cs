namespace CarShopView.Querys;

public interface IQueryCustomers : IQueryPagination {
    public string?  Name { get; set; }
    public string?  DocType { get; set; }
}