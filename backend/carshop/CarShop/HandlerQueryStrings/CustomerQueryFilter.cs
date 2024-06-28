namespace CarShop.HandlerQueryStrings;

public class CustomerQueryFilter : CarShopPagination {
    public string? name { get; set; }
    public string? docType { get; set; }

    public override string ToString()
    {
        string msg = "[";
        if (name is not null) {
            msg += $" {name}";
        }
        if (docType is not null) {
            msg += $" {docType}";
        }
        return $"{msg} ]";
    }
}