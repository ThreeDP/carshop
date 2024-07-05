using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarShop.Models;
using CarShop.Validations;

namespace CarShop.DTO;

public class CustomerDTO
{
    [JsonPropertyName("customer_id")]
    public int          Id { get; set; }

    [Required]
    [JsonPropertyName("name")]
    public string?      Name { get; set; }

    [JsonPropertyName("url_profile")]
    public string?      Photo { get; set; }

    [Required]
    [JsonPropertyName("document_type")]
    [DocTypeAttribute]
    public string?      DocType { get; set; }
    
    [Required]
    [JsonPropertyName("document_number")]
    public string?      DocNumber { get; set; }
    
    [Required]
    [JsonPropertyName("cellphone")]
    public string?      Phone { get; set; }

    public CustomerDTO(CustomerDB? other) {
        if (other is not null) {
            this.Id = other.Id;
            this.Name = other.Name;
            this.Photo = other.PerfilPhoto;
            this.DocType = other.DocType;
            this.DocNumber = other.DocNumber;
            this.Phone = other.Phone;
        }
    }

    public CustomerDTO(){}
}