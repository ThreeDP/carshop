using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CarShop.Models;

[Table("customers")]
public class CustomerDB : IEquatable<CustomerDB>
{
    public CustomerDB() {
        FinancialTransactions = new Collection<FinancialTransactionsDB>();
    }

    [Key]
    [Column("customer_id")]
    [JsonPropertyName("customer_id")]
    public int Id { get; set; }

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

    public static bool operator ==(CustomerDB x, CustomerDB y)
    {
        if (ReferenceEquals(x, y)) 
            return true;
        if (ReferenceEquals(x, null)) 
            return false;
        if (ReferenceEquals(y, null))
            return false;
        return x.Equals(y);
    }

    public static bool operator !=(CustomerDB x, CustomerDB y) => !(x == y);

    public bool Equals(CustomerDB other)
    {
        if (ReferenceEquals(other, null))
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return this.Id == other.Id &&
            this.Name == other.Name &&
            this.PerfilPhoto == other.PerfilPhoto &&
            this.DocType == other.DocType &&
            this.DocNumber == other.DocNumber &&
            this.Phone == other.Phone;
    }
    public override bool Equals(object obj) => Equals(obj as CustomerDB);

    public override int GetHashCode()
    {
        unchecked
        {
            int hashCode = Name.GetHashCode();
            hashCode = (hashCode * 397) ^ DocNumber.GetHashCode();
            return hashCode;
        }
    }
}
