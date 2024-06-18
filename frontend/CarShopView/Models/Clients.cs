using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarShopView.Models;

public class Client
{
  [JsonPropertyName("name")]
  public string?            Name { get; set; }
  
  [JsonPropertyName("url_profile")]
  public string?            UrlImage { get; set; }

  [JsonPropertyName("document_type")]
  public string?            DocType { get; set; }

  [JsonPropertyName("document_number")]
  public string?            DocNumber { get; set; }

  [JsonPropertyName("cellphone")]
  public string?            CellPhone { get; set; }
}