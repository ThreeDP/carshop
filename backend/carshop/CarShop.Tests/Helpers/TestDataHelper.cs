namespace CarShop.Tests.Helpers;

public static class TestDataHelper
{
    public static List<CustomerDB> GetFakeCustomerList()
    {
        return new List<CustomerDB>()
        {
            new CustomerDB
            {
                Id = 1,
                Name = "John Doe",
                PerfilPhoto = "/images/1/x.png",
                DocType = "CPF",
                DocNumber = "000.000.000-06",
                Phone = "5511987362345",
            },
            new CustomerDB
            {
                Id = 2,
                Name = "Mark Luther",
                PerfilPhoto = "/images/2/x.png",
                DocType = "CNPJ",
                DocNumber = "00000000000100",
                Phone = "5511987365345",
            }
        };
    }
}