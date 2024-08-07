using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using CarShop.DTO;
using Microsoft.Extensions.Options;

namespace CarShop.Models;

[Table("vehicles")]
public class VehicleDB : IValidatableObject
{
    public VehicleDB()
    {
        FinancialTransactions = new Collection<FinancialTransactionsDB>();
        VehicleImages = new Collection<VehicleImageDB>();
    }

    public VehicleDB(VehicleDTO? other) {
        if (other is not null) {
            Id = other.Id;
            Renavan = other.Renavan;
            LicensePlate = other.LicensePlate;
            Brand = other.Brand;
            Model = other.Model;
            ModelYear = other.ModelYear;
            VehicleType = other.VehicleType;
            YearManufacture = other.YearManufacture;
            Description = other.Description;
            Situation = other.Situation;
        }
        FinancialTransactions = new Collection<FinancialTransactionsDB>();
        VehicleImages = new Collection<VehicleImageDB>();
    }

    public VehicleDB Copy(VehicleDTO? other) {
        if (other is not null) {
            Renavan = other.Renavan;
            LicensePlate = other.LicensePlate;
            Brand = other.Brand;
            Model = other.Model;
            ModelYear = other.ModelYear;
            VehicleType = other.VehicleType;
            YearManufacture = other.YearManufacture;
            Description = other.Description;
            Situation = other.Situation;
        }
        return this;
    }

    public VehicleDB(VehicleDB other) {
        Id = other.Id;
        Renavan = other.Renavan;
        LicensePlate = other.LicensePlate;
        Brand = other.Brand;
        Model = other.Model;
        ModelYear = other.ModelYear;
        VehicleType = other.VehicleType;
        YearManufacture = other.YearManufacture;
        RegistrationDate = other.RegistrationDate;
        ChangeDate = other.ChangeDate;
        Description = other.Description;
        Situation = other.Situation;
        FinancialTransactions = new Collection<FinancialTransactionsDB>();
        VehicleImages = new Collection<VehicleImageDB>();
    }

    [Key]
    [Column("vehicle_id")]
    public int          Id { get; set; }

    [Required]
    [Column("renavan", TypeName="varchar(12)")]
    public string?      Renavan { get; set; }

    [Required]
    [Column("license_plate", TypeName="varchar(8)")]
    public string?      LicensePlate { get; set; }

    [Required]
    [Column("brand", TypeName="varchar(40)")]
    public string?      Brand { get; set; }

    [Required]
    [Column("model", TypeName="varchar(40)")]
    public string?      Model { get; set; }

    [Required]
    [Column("model_year")]
    public DateTime?     ModelYear { get; set; }
    
    [Required]
    [Column("vehicle_type")]
    public string?      VehicleType { get; set; }
    
    [Required]
    [Column("year_manufacture")]
    public DateTime?    YearManufacture { get; set; }
    
    [JsonIgnore]
    [Column("registration_date")]
    public DateTime?    RegistrationDate { get; set; }
    
    [JsonIgnore]
    [Column("change_date")]
    public DateTime?    ChangeDate { get; set; }
    
    [Required]
    [Column("description")]
    public string?      Description { get; set; }

    [Required]
    [Column("situation", TypeName="varchar(20)")]
    public string?      Situation { get; set; }

    [JsonPropertyName("vehicle_images")]
    public ICollection<VehicleImageDB> VehicleImages { get; set; }

    [JsonIgnore]
    public ICollection<FinancialTransactionsDB> FinancialTransactions { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext vctx) {
        if (!string.IsNullOrEmpty(this.Situation)) {
            var situation = this.Situation.ToUpper().Normalize();
            if (situation != "DISPONIVEL" && situation != "INDISPONIVEL") {
                yield return new ValidationResult("Situação inválida.",
                new[] {
                    nameof(this.Situation)
                });
            }
        }
    }
}