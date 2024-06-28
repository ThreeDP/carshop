namespace CarShop.HandlerQueryStrings;

public class CarShopPagination {
    protected const int maxPageSize = 50;
    protected int _pageSize = 50;
    public int PageNumber { get; set; } = 1;
    public int PageSize {
        get {
            return _pageSize;
        }
        set {
            _pageSize = (value > maxPageSize)? maxPageSize : value;
        }
    }
}