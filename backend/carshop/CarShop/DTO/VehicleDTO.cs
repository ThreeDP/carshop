using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using CarShop.Validations;
using Microsoft.AspNetCore.Mvc;

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

public class VehicleDTO : IVehicleDTO, IValidatableObject {
    private static readonly string[] situationOptions = { 
        "DISPONIVEL", "INDISPONIVEL", "VENDIDO"
    };

    [JsonPropertyName("vehicle_id")]
    public int          Id { get; set; }

    [Required]
    [JsonPropertyName("renavan")]
    public string?      Renavan { get; set; }

    [Required]
    [JsonPropertyName("license_plate")]
    public string?      LicensePlate { get; set; }

    [Required]
    [JsonPropertyName("brand")]
    public string?      Brand { get; set; }

    [Required]
    [JsonPropertyName("model")]
    public string?      Model { get; set; }
    
    [Required]
    [JsonPropertyName("model_year")]
    public DateTime?    ModelYear { get; set; }

    [Required]
    [JsonPropertyName("vehicle_type")]
    public string?      VehicleType { get; set; }

    [Required]
    [JsonPropertyName("year_manufacture")]
    public DateTime?    YearManufacture { get; set; }

    [JsonPropertyName("description")]
    public string?      Description { get; set; }

    [Required]
    [JsonPropertyName("situation")]
    public string?      Situation { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validContext) {
        if (!string.IsNullOrEmpty(this.Situation)) {
            var situation = this.Situation.ToUpper().Normalize();
            if (!situationOptions.Contains(situation)) {
                yield return new ValidationResult("Situação inválida para transação.",
                    new[] {
                        nameof(this.Situation)
                });
            }
        }
    }
}