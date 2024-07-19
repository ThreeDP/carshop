using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarShopView.Models;

public interface IUser
{
  public string?            Username { get; set; }
  public string?            Password { get; set; }
}

public class User : IUser
{
  [JsonPropertyName("username")]
  [Required(ErrorMessage = "informe seu nome de usuário.")]
  public string?            Username { get; set; }
  
  [JsonPropertyName("password")]
  [Required(ErrorMessage = "informe sua senha.")]
  public string?            Password { get; set; }
}