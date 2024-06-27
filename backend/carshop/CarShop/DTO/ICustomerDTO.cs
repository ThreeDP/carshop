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
