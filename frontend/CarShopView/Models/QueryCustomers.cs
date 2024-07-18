namespace CarShopView.Models;

public class QueryCustomers {
    public string?  Name { get; set; }
    public string?  DocType { get; set; }
    public int?     PageNumber { get; set; } = 1;
    public int?     PageSize { get; set; } = 2;
}