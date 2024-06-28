namespace CarShop.HandlerQueryStrings;

public class TransactionQueryFilter : CarShopPagination {
    private decimal _minValue = 0;
    private decimal? _maxValue = null;
    public string? Type { get; set; }
    public decimal MinValue {
        get {
            return _minValue;
        }
        set {
            _minValue = (value < 0)? _minValue : value;
        }
    }
    public decimal? MaxValue {
        get {
            return _maxValue;
        }
        set {
            _maxValue = (value < 0)? _maxValue : value; 
        }
    }
}