namespace CarShop.HandlerQueryStrings;

public class VehicleQueryFilter : CarShopPagination {    
    public string?      Renavan { get; set; }
    public string?      LicensePlate { get; set; }
    public string?      Brand { get; set; }
    public string?      Model { get; set; }
    public DateTime?    ModelYear { get; set; }
    public string?      VehicleType { get; set; }
    public string?      Situation { get; set; }

    public override string ToString()
    {
        var msg = "[";
        if (Renavan is not null)
            msg += $" {Renavan}";
        if (LicensePlate is not null)
            msg += $" {LicensePlate}";
        if (Brand is not null)
            msg += $" {Brand}";
        if (Model is not null)
            msg += $" {Model}";
        if (ModelYear is not null)
            msg += $" {ModelYear}";
        if (VehicleType is not null)
            msg += $" {VehicleType}";
        if (Situation is not null)
            msg += $" {Situation}";
        return $"{msg} ]";
    }
}