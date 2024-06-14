using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace CarShop.Models;

[Table("clients")]
public class ClientDB
{
    public ClientDB()
    {
        FinancialTransactions = new Collection<FinancialTransactionsDB>();
    }

    [Key]
    [Column("client_id")]
    public int ClientDBId { get; set; }

    [Required]
    [Column("name")]
    [StringLength(80)]
    [JsonPropertyName("Nome")]
    public string? Name { get; set; }

    [Column("image_url")]
    [StringLength(300)]
    [JsonPropertyName("Url foto de Perfil")]
    public string? PerfilPhoto { get; set; }

    [Required]
    [Column("doc_type")]
    [StringLength(4)]
    [JsonPropertyName("Tipo do documento")]
    public string? DocType { get; set; }

    [Required]
    [Column("doc_number")]
    [StringLength(20)]
    [JsonPropertyName("Número do documento")]
    public string? DocNumber { get; set; }

    [Required]
    [Column("phonenumber")]
    [StringLength(15)]
    [JsonPropertyName("Número de Telefone")]
    public string? Phone { get; set; }

    [JsonIgnore]
    public ICollection<FinancialTransactionsDB> FinancialTransactions { get; set; }
}

[Table("financial_transactions")]
public class FinancialTransactionsDB
{
    [Key]
    [Column("financial_transation_id")]
    public int FinancialTransactionDBId { get; set; }

    [Column("value", TypeName="decimal(10,2)")]
    public decimal Value { get; set; }

    [Column("type_transation")]
    [StringLength(6)]
    public string? FinancialTransactionType { get; set; }

    [Column("categoria_id")]
    public int CategoriaId { get; set; }
    public ClientDB? Client { get; set; }
}