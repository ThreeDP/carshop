using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarShopView.Models;

public interface ITokenModel
{
  public string?            Token { get; set; }
  public string?            RefreshToken { get; set; }
}

public class TokenModel : ITokenModel
{
  [JsonPropertyName("token")]
  public string?            Token { get; set; }
  
  [JsonPropertyName("refreshToken")]
  public string?            RefreshToken { get; set; }
}