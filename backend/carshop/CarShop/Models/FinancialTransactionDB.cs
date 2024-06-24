using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using CarShop.DTO;

namespace CarShop.Models;

[Table("financial_transactions")]
public class FinancialTransactionsDB
{

    public FinancialTransactionsDB(){}

    public FinancialTransactionsDB(TransactionRequestDTO other) {
        if (other is not null) {
            Value = other.Value;
            FinancialTransactionType = other.Type;
            CustomerId = other.CustomerId;
            VehicleId = other.VehicleId;
            if (other != null) {
                Customer = other.Customer != null ? new CustomerDB(other.Customer) : null;
                Vehicle = other.Vehicle != null ? new VehicleDB(other.Vehicle) : null;
            }
        }
    }
    public FinancialTransactionsDB(FinancialTransactionsDB other) {
        Id = other.Id;
        Value = other.Value;
        FinancialTransactionType = other.FinancialTransactionType;
        CustomerId = other.CustomerId;
        VehicleId = other.VehicleId;
        if (other != null) {
            Customer = other.Customer != null ? new CustomerDB(other.Customer) : other.Customer;
            Vehicle = other.Vehicle;
        }
    }

    [Key]
    [Column("financial_transation_id")]
    [JsonPropertyName("financial_transation_id")]
    [JsonIgnore]
    public int Id { get; set; }

    [Required]
    [Column("value", TypeName="decimal(10,2)")]
    [JsonPropertyName("value")]
    public decimal Value { get; set; }

    [Required]
    [StringLength(6)]
    [Column("type_transation")]
    [JsonPropertyName("type_transation")]
    public string? FinancialTransactionType { get; set; }

    [Required]
    [Column("customer_id")]
    [JsonPropertyName("customer_id")]
    public int CustomerId { get; set; }

    [Required]
    [Column("vehicle_id")]
    [JsonPropertyName("vehicle_id")]
    public int VehicleId { get; set; }

    public CustomerDB? Customer { get; set; }

    public VehicleDB? Vehicle { get; set; }

    public override string ToString()
    {
        string msg = $"[ Id: {Id}, Value: {Value}, Type: {FinancialTransactionType}, customerId: {CustomerId}, vehicleId: {VehicleId},";
        if (Customer is not null)
            msg += $"Customer: [ Id: {this.Customer.Id}, Nanme: {this.Customer.Name} ], ";
        else
            msg += "Customer: [null], ";
        if (Vehicle is not null)
            msg += $"Vehicle: [ Id: {this.Vehicle.Id}, Nanme: {this.Vehicle.Brand} ] ]";
        else
            msg += "Vehicle: [null] ]";
        return msg;
    }
}