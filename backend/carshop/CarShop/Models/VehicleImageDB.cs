using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using CarShop.DTO;

namespace CarShop.Models;

[Table("vehicle_image")]
public class VehicleImageDB {
    [Key]
    [Column("vehicle_id")]
    [JsonPropertyName("vehicle_image_id")]
    [JsonIgnore]
    public int      VehicleImageDBId { get; set; }

    [Required]
    [Column("vehicle_id")]
    [JsonPropertyName("vehicle_id")]
    public int      VehicleDBId { get; set; }

    [Required]
    [Column("url", TypeName="varchar(300)")]
    [JsonPropertyName("url")]
    public string?  Url { get; set; }

    [JsonIgnore]
    public VehicleDB? Vehicle { get; set; }

    public VehicleImageDB(){}

    public VehicleImageDB(IVehicleImageDTO? other) {
        if (other is not null) {
            this.VehicleImageDBId = other.Id;
            this.VehicleDBId = other.Id;
            this.Url = other.Image;
        }
    }
}