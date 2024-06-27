namespace CarShop.DTO;

public interface IVehicleDTO {
    public int          Id { get; set; }
    public string?      Renavan { get; set; }
    public string?      LicensePlate { get; set; }
    public string?      Brand { get; set; }
    public string?      Model { get; set; }
    public DateTime?    ModelYear { get; set; }
    public string?      VehicleType { get; set; }
    public DateTime?    YearManufacture { get; set; }
    public string?      Description { get; set; }
    public string?      Situation { get; set; }
}