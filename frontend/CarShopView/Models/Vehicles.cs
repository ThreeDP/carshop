using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarShopView.Models;

public class Vehicle {
    [JsonPropertyName("vehicle_id")]
    public int                  Id { get; set; }

    [JsonPropertyName("renavan")]
    public string?              Renavan { get; set; }
    
    [JsonPropertyName("license_plate")]
    public string?              License { get; set; }

    [JsonPropertyName("brand")]
    public string?              Brand { get; set; }
    
    [JsonPropertyName("model")]
    public string?              Model { get; set; }

    [JsonPropertyName("model_year")]
    public DateTime             ModelYear { get; set; }

    [JsonPropertyName("vehicle_type")]
    public string?              VehicleType { get; set; }

    [JsonPropertyName("year_manufacture")]
    public DateTime             YearManufacture { get; set; }

    [JsonPropertyName("description")]
    public string?              Description { get; set; }
    
    [JsonPropertyName("situation")]
    public string?              Situation { get; set; }
}