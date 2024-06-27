namespace CarShop.DTO;

public interface ITransactionDTO {
    public int              Id { get; set; }
    public decimal          Value { get; set; }
    public string?          Type { get; set; }
    public int              CustomerId { get; set; }
    public int              VehicleId { get; set; }
    public ICustomerDTO?     Customer { get; set; }
    public IVehicleDTO?      Vehicle { get; set; }
}