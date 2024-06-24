using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarShopView.Models;
public class Transaction
{
    [JsonPropertyName("value")]
    public decimal  Value { get; set; }
    [JsonPropertyName("type_transation")]
    public string?  TypeTransaction { get; set; }
    [JsonPropertyName("customer_id")]
    public int      CustomerId { get; set; }
    [JsonPropertyName("vehicle_id")]
    public int      VehicleId { get; set; }
    public Customer? Customer { get; set; }
    public Vehicle?     Vehicle { get; set; }

    public override string ToString() {
        string msg = $"[ value: {Value}, Type: {TypeTransaction}, cId: {CustomerId}, vId: {VehicleId} ]";
        if (Customer is not null)
            msg += $"\nCustomer: [id: {Customer.Id}, name: {Customer.Name}]";
        if (Vehicle is not null)
            msg += $"\nVehicle: [id: {Vehicle.Id}, Brand: {Vehicle.Brand}]";
        return msg;
    }
}