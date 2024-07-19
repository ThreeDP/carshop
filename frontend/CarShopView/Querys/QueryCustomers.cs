namespace CarShopView.Querys;

public class QueryCustomers : QueryPagination, IQueryCustomers {
    public string?  Name { get; set; }
    public string?  DocType { get; set; }
}