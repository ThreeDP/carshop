using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarShop.Models;

[Table("clients")]
public class ClientDB
{
    public ClientDB() {
        FinancialTransactions = new Collection<FinancialTransactionsDB>();
    }

    [Key]
    [Column("client_id")]
    [JsonPropertyName("client_id")]
    public int ClientDBId { get; set; }

    [Required]
    [Column("name")]
    [StringLength(80)]
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [Column("image_url")]
    [StringLength(300)]
    [JsonPropertyName("url_profile")]
    public string? PerfilPhoto { get; set; }

    [Required]
    [Column("doc_type")]
    [StringLength(4)]
    [JsonPropertyName("document_type")]
    public string? DocType { get; set; }

    [Required]
    [Column("doc_number")]
    [StringLength(20)]
    [JsonPropertyName("document_number")]
    public string? DocNumber { get; set; }

    [Required]
    [Column("phonenumber")]
    [StringLength(15)]
    [JsonPropertyName("cellphone")]
    public string? Phone { get; set; }

    [JsonIgnore]
    public ICollection<FinancialTransactionsDB> FinancialTransactions { get; set; }

    public override string ToString() {
        return $"name: {this.Name}";
    }
}
