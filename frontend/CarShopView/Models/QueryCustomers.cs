namespace CarShopView.Models;

public interface IQueryCustomers {
    public string?  Name { get; set; }
    public string?  DocType { get; set; }
    public int?     PageNumber { get; set; }
    public int?     PageSize { get; set; }
}

public class QueryCustomers : IQueryCustomers {
    public string?  Name { get; set; }
    public string?  DocType { get; set; }
    public int?     PageNumber { get; set; } = 1;
    public int?     PageSize { get; set; } = 2;
}