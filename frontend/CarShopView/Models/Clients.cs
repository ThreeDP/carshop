using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarShopView.Models;

public class Customer
{
  [JsonPropertyName("customer_id")]
  public int                Id { get; set; }

  [JsonPropertyName("name")]
  [Required(ErrorMessage = "informe seu nome.")]
  public string?            Name { get; set; }
  
  [JsonPropertyName("url_profile")]
  public string?            UrlImage { get; set; }

  [Required(ErrorMessage = "informe o tipo do seu documento.")]
  [JsonPropertyName("document_type")]
  public string?            DocType { get; set; }

  [Required(ErrorMessage = "informe o número do seu documento.")]
  [JsonPropertyName("document_number")]
  public string?            DocNumber { get; set; }

  [Required(ErrorMessage = "informe seu número de telefone ou celular.")]
  [JsonPropertyName("cellphone")]
  public string?            CellPhone { get; set; }
}