using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarShop.Models;

namespace CarShop.DTO;

public class TransactionResponseDTO {
    [JsonPropertyName("id")]
    public int              Id { get; set; }

    [Required]
    [JsonPropertyName("value")]
    public decimal          Value { get; set; }

    [Required]
    [JsonPropertyName("type_transation")]
    public string?          Type { get; set; }

    [Required]
    public string?          Customer { get; set; }
    [Required]
    public string?          Vehicle { get; set; }

    public TransactionResponseDTO(){}
    public TransactionResponseDTO(FinancialTransactionsDB? other) {
        if (other is not null) {
            this.Id = other.Id;
            this.Type = other.FinancialTransactionType;
            this.Value = other.Value;
            this.Customer = $"{other.Customer?.Name} / {other.Customer?.DocNumber}";
            this.Vehicle = $"{other.Vehicle?.Model} | {other.Vehicle?.Brand} | {other.Vehicle?.LicensePlate}";
        }
    }
}