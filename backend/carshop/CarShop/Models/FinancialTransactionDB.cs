using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarShop.Models;

[Table("financial_transactions")]
public class FinancialTransactionsDB
{
    [Key]
    [Column("financial_transation_id")]
    [JsonPropertyName("financial_transation_id")]
    [JsonIgnore]
    public int FinancialTransactionDBId { get; set; }

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

    [JsonIgnore]
    public CustomerDB? Customer { get; set; }

    [JsonIgnore]
    public VehicleDB? Vehicle { get; set; }
}