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

    public CustomerDTO?     Customer { get; set; }
    public VehicleDTO?      Vehicle { get; set; }
}

public class TransactionDTO : IValidatableObject {
    private static readonly string[] transactionTypes = {
        "VENDA", "COMPRA"
    };

    [JsonPropertyName("id")]
    public int              Id { get; set; }

    [Required]
    [JsonPropertyName("value")]
    public decimal          Value { get; set; }

    [JsonIgnore]
    public string?          Type { get; set; }

    [Required]
    [JsonPropertyName("customer_id")]
    public int              CustomerId { get; set; }

    [Required]
    [JsonPropertyName("vehicle_id")]
    public int              VehicleId { get; set; }

    public CustomerDTO?     Customer { get; set; }
    public VehicleDTO?      Vehicle { get; set; }

    public TransactionDTO(){}
    public TransactionDTO(FinancialTransactionsDB? other) {
        if (other is not null) {
            this.Id = other.Id;
            this.Value = other.Value;
            this.Type = other.FinancialTransactionType;
            this.CustomerId = other.CustomerId;
            this.VehicleId = other.VehicleId;
            this.Customer = new CustomerDTO(other.Customer);
            this.Vehicle = new VehicleDTO(other.Vehicle);
        }
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext vd) {
        if (!string.IsNullOrEmpty(this.Type)) {
            var type = this.Type.ToUpper().Normalize();
            if (!transactionTypes.Contains(type)) {
                yield return new ValidationResult("Tipo de transação inválida.",
                    new []{
                        nameof(this.Type)
                });
            }
        }
        if (this.Vehicle is not null && this.VehicleId != this.Vehicle.Id) {
            yield return new ValidationResult("Id inválido.",
                new []{
                    nameof(this.VehicleId),
                    nameof(this.Vehicle.Id)
            });
        }
    }
}