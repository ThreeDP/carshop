using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq.EntityFrameworkCore;
using CarShop.Tests.Helpers;

namespace CarShop.Tests.CustomersControllerTest.Post;

public class PostCustomer
{
    private readonly Mock<CarShopDataContext> contextMock;
    private TestDataHelper DataHelper;
    public PostCustomer() {
        contextMock = new Mock<CarShopDataContext>();
        DataHelper = new TestDataHelper();
    }

    [Fact]
    public async Task Test_PostCustomer_WhenCalled_CreateACustomer_and_ReturnNewCustomer()
    {
        // Arrange
        var expected = DataHelper.GetFakeCustomerList;
        var newCustomer = new CustomerDB {
            Id = 4,
            Name = "Jonas Cury",
            PerfilPhoto = "/images/4/x.png",
            DocType = "CPF",
            DocNumber = "109.000.000-06",
            Phone = "5511909362345",
        };
        contextMock.Setup<DbSet<CustomerDB>?>(x => x.Customers)
            .ReturnsDbSet(DataHelper.GetFakeCustomerList);

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = customerController.Post(newCustomer);

        //Assert
        var objResult = Assert.IsType<CreatedAtRouteResult>(actionResult);
        Assert.Equal("ObterCliente", objResult.RouteName);
        Assert.Equal(newCustomer, objResult.Value);
    }

    // [Fact]
    // public async Task Test_PostCustomer_WhenCalled_ReturnsBadRequest()
    // {
    //     // Arrange
    //     // var newCustomer = new CustomerDB ;
    //     var expected = DataHelper.GetFakeCustomerList;
    //     contextMock.Setup<DbSet<CustomerDB>?>(x => x.Customers)
    //         .ReturnsDbSet(DataHelper.GetFakeCustomerList);

    //     //Act
    //     CustomersController customerController = new(contextMock.Object);
    //     var actionResult = customerController.Post(new {
    //         Id = 4,
    //         Name = "Jonas Cury",
    //         PerfilPhoto = "/images/4/x.png",
    //         DocNumber = "109.000.000-06",
    //         Phone = "5511909362345",
    //     });

    //     //Assert
    //     var result = Assert.IsType<BadRequestResult>(actionResult);
    // }

    [Fact]
    public async Task Test_PostCustomer_WhenCalled_ReturnsServerError()
    {
        // Arrange
        var newCustomer = new CustomerDB {
            Id = 4,
            Name = "Jonas Cury",
            PerfilPhoto = "/images/4/x.png",
            DocType = "CPF",
            DocNumber = "109.000.000-06",
            Phone = "5511909362345",
        };
        contextMock.Setup<DbSet<CustomerDB>?>(x => x.Customers)
            .Throws<InvalidOperationException>();

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = customerController.Post(newCustomer);

        //Assert
        var stc = Assert.IsType<ObjectResult>(actionResult);
        Assert.Equal(500, stc.StatusCode);
    }
}
