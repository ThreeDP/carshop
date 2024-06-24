using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

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