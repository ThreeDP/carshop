using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarShop.Models;

namespace CarShop.DTO;

public class VehicleImageDTO {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Required]
    [JsonPropertyName("image")]
    public string? Image { get; set; }

    [Required]
    [JsonPropertyName("vehicle_id")]
    public int  VehicleId { get; set; }

    public VehicleImageDTO(){}
    public VehicleImageDTO(VehicleImageDB? other) {
        if (other is not null) {
            this.Id = other.VehicleImageDBId;
            this.Image = other.Url;
        }
    }
}