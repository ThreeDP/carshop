using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarShop.Models;

namespace CarShop.DTO;

public class VehicleDTO : IValidatableObject {
    private static readonly string[] situationOptions = { 
        "DISPONIVEL", "INDISPONIVEL", "VENDIDO"
    };

    public VehicleDTO(){}
    public VehicleDTO(VehicleDB? other) {
        if (other is not null) {
            this.Id = other.Id;
            this.Renavan = other.Renavan;
            this.LicensePlate = other.LicensePlate;
            this.Brand = other.Brand;
            this.Model = other.Model;
            this.ModelYear = other.ModelYear;
            this.VehicleType = other.VehicleType;
            this.YearManufacture = other.YearManufacture;
            this.Description = other.Description;
            this.Situation = other.Situation;
        }
    }

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