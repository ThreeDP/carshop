using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using CarShop.Validations;
using Microsoft.AspNetCore.Mvc;

namespace CarShop.DTO;

public interface ICustomerDTO
{
    public int          Id { get; set; }
    public string?      Name { get; set; }
    public string?      Photo { get; set; }
    public string?      DocType { get; set; }
    public string?      DocNumber { get; set; }
    public string?      Phone { get; set; }
}

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
    [JsonPropertyName("cellphone")]
    public string?      Phone { get; set; }
}