using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarShop.Models;

namespace CarShop.DTO;

public class TransactionRequestDTO {
    [JsonPropertyName("id")]
    public int              Id { get; set; }

    [Required]
    [JsonPropertyName("value")]
    public decimal          Value { get; set; }

    [Required]
    [JsonPropertyName("type_transation")]
    public string?          Type { get; set; }

    [Required]
    [JsonPropertyName("customer_id")]
    public int              CustomerId { get; set; }

    [Required]
    [JsonPropertyName("vehicle_id")]
    public int              VehicleId { get; set; }

    public ICustomerDTO?     Customer { get; set; }
    public IVehicleDTO?      Vehicle { get; set; }
}

public class TransactionDTO : ITransactionDTO {
    [JsonPropertyName("id")]
    public int              Id { get; set; }

    [Required]
    [JsonPropertyName("value")]
    public decimal          Value { get; set; }

    [Required]
    [JsonPropertyName("type_transation")]
    public string?          Type { get; set; }

    [Required]
    [JsonPropertyName("customer_id")]
    public int              CustomerId { get; set; }

    [Required]
    [JsonPropertyName("vehicle_id")]
    public int              VehicleId { get; set; }

    public ICustomerDTO?     Customer { get; set; }
    public IVehicleDTO?      Vehicle { get; set; }

    public TransactionDTO(FinancialTransactionsDB? other) {
        if (other is not null) {
            this.Id = other.Id;
            this.Value = other.Value;
            this.Type = other.FinancialTransactionType;
            this.CustomerId = other.CustomerId;
            this.VehicleId = other.VehicleId;
            if (other.Customer is not null)
                this.Customer = new CustomerDTO(other.Customer);
            if (other.Vehicle is not null)
                this.Vehicle = new VehicleDTO(other.Vehicle);
        }
    }
}