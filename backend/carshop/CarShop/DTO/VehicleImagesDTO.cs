using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarShop.Models;

namespace CarShop.DTO;

public class VehicleImageDTO : IVehicleImageDTO {
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [Required]
    [JsonPropertyName("image")]
    public string? Image { get; set; }

    public VehicleImageDTO(VehicleImageDB? other) {
        if (other is not null) {
            this.Id = other.VehicleImageDBId;
            this.Image = other.Url;
        }
    }
}