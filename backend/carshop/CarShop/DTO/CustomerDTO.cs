using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CarShop.Models;

namespace CarShop.DTO;

public class CustomerDTO : ICustomerDTO
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
    public string?      DocType { get; set; }
    
    [Required]
    [JsonPropertyName("document_number")]
    public string?      DocNumber { get; set; }
    
    [Required]
    [JsonIgnore]
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
}