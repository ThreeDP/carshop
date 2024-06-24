using System;
namespace CarShop.Tests.Helpers;

public class TestDataHelper
{
    private List<CustomerDB>    customers;
    public TestDataHelper() {
        customers = new List<CustomerDB>()
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
                },
                new CustomerDB
                {
                    Id = 3,
                    Name = "Monica King",
                    PerfilPhoto = "/images/3/x.png",
                    DocType = "CNPJ",
                    DocNumber = "01000000000100",
                    Phone = "5511987369345",
                }
            };
    }

    public List<CustomerDB> GetFakeCustomerList { get { return customers; } }

    public List<CustomerDB> GetFakeNullCustomerList() {
        return new List<CustomerDB>();
    }
    public List<CustomerDB> GetFakeServerError() {
        return new List<CustomerDB>();
    }
}