using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq.EntityFrameworkCore;
using CarShop.Tests.Helpers;

namespace CarShop.Tests.CustomersControllerTest.Get;

public class GetCustomersById
{
    private readonly Mock<CarShopDataContext> contextMock;
    public GetCustomersById() {
        contextMock = new Mock<CarShopDataContext>();
    }

    [Fact]
    public async Task Test_GetCustomerById_WhenCalled_ReturnsACustomer()
    {
        // Arrange
        var expected = TestDataHelper.GetFakeCustomerList()[0];
        contextMock.Setup<DbSet<CustomerDB>>(x => x.Customers)
            .ReturnsDbSet(TestDataHelper.GetFakeCustomerList());

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = customerController.Get(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var customer = Assert.IsType<CustomerDB>(okResult.Value);
        Assert.NotNull(customer);
        Assert.Equal(expected, customer);
    }

        [Fact]
    public async Task Test_GetCustomerById_WhenCalled_ReturnsNotFound()
    {
        // Arrange
        contextMock.Setup<DbSet<CustomerDB>>(x => x.Customers)
            .ReturnsDbSet(TestDataHelper.GetFakeCustomerList());

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = customerController.Get(99);

        // Assert
        var okResult = Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task Test_GetCustomerById_WhenCalled_ReturnsServerError()
    {
        // Arrange
        contextMock.Setup<DbSet<CustomerDB>?>(x => x.Customers)
            .Throws<InvalidOperationException>();

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = await customerController.Get();

        //Assert
        var stc = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(500, stc.StatusCode);
    }

}
