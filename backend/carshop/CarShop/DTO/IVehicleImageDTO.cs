namespace CarShop.DTO;

public interface IVehicleImageDTO {
    public int Id { get; set; }
    public int  VehicleId { get; set; }
    public string? Image { get; set; }
}