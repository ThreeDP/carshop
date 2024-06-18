using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarShop.Models;

[Table("vehicles")]
public class VehicleDB
{
    public VehicleDB()
    {
        FinancialTransactions = new Collection<FinancialTransactionsDB>();
        VehicleImages = new Collection<VehicleImageDB>();
    }

    [Key]
    [Column("vehicle_id")]
    [JsonPropertyName("vehicle_id")]
    [JsonIgnore]
    public int          VehicleDBId { get; set; }

    [Required]
    [Column("renavan", TypeName="varchar(12)")]
    [JsonPropertyName("renavan")]
    public string?      Renavan { get; set; }

    [Required]
    [Column("license_plate", TypeName="varchar(8)")]
    [JsonPropertyName("license_plate")]
    public string?      LicensePlate { get; set; }

    [Required]
    [Column("brand", TypeName="varchar(40)")]
    [JsonPropertyName("brand")]
    public string?      Brand { get; set; }

    [Required]
    [Column("model", TypeName="varchar(40)")]
    [JsonPropertyName("model")]
    public string?      Model { get; set; }

    [Required]
    [Column("model_year")]
    [JsonPropertyName("model_year")]
    public DateTime?     ModelYear { get; set; }
    
    [Required]
    [Column("vehicle_type")]
    [JsonPropertyName("vehicle_type")]
    public string?      VehicleType { get; set; }
    
    [Required]
    [Column("year_manufacture")]
    [JsonPropertyName("year_manufacture")]
    public DateTime?    YearManufacture { get; set; }
    
    [JsonIgnore]
    [Column("registration_date")]
    public DateTime?    RegistrationDate { get; set; }
    
    [JsonIgnore]
    [Column("change_date")]
    public DateTime?    ChangeDate { get; set; }
    
    [Required]
    [Column("description")]
    [JsonPropertyName("description")]
    public string?      Description { get; set; }

    [Required]
    [Column("situation", TypeName="varchar(20)")]
    [JsonPropertyName("situation")]
    public string?      Situation { get; set; }

    [JsonIgnore]
    public ICollection<VehicleImageDB> VehicleImages { get; set; }

    [JsonIgnore]
    public ICollection<FinancialTransactionsDB> FinancialTransactions { get; set; }
}