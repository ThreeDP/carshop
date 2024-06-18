using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarShopView.Models;
public class Transaction
{
    public decimal  Value { get; set; }
    [JsonPropertyName("type_transation")]
    public string?  TypeTransaction { get; set; }
    [JsonPropertyName("client_id")]
    public int      ClientId { get; set; }
    [JsonPropertyName("vehicle_id")]
    public int      VehicleId { get; set; }
}