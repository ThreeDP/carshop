using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq.EntityFrameworkCore;
using CarShop.Tests.Helpers;

namespace CarShop.Tests.CustomersControllerTest.Get;

public class GetCustomerByName
{
    private readonly Mock<CarShopDataContext> contextMock;
    private TestDataHelper DataHelper;
    public GetCustomerByName() {
        contextMock = new Mock<CarShopDataContext>();
        DataHelper = new TestDataHelper();
    }

    [Fact]
    public async Task Test_GetCustomersByIdName_WhenCalled_ReturnsACustomerInListAsync()
    {
        // Arrange
        var expected = DataHelper.GetFakeCustomerList;
        contextMock.Setup<DbSet<CustomerDB>?>(x => x.Customers)
            .ReturnsDbSet(DataHelper.GetFakeCustomerList);

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = await customerController.GetAsync("John Doe");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var customer = Assert.IsType<List<CustomerDB>>(okResult.Value);
        Assert.NotNull(customer);
        Assert.Equal(expected[0], customer.ElementAt(0));
    }

    [Fact]
    public async Task Test_GetCustomersByIdName_WhenCalled_ReturnsCustomersListAsync()
    {
        // Arrange
        var expected = DataHelper.GetFakeCustomerList;
        contextMock.Setup<DbSet<CustomerDB>?>(x => x.Customers)
            .ReturnsDbSet(DataHelper.GetFakeCustomerList);

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = await customerController.GetAsync("m");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var customers = Assert.IsType<List<CustomerDB>>(okResult.Value);
        Assert.NotNull(customers);
        Assert.Equal(expected[1], customers.ElementAt(0));
        Assert.Equal(expected[2], customers.ElementAt(1));
    }

    [Fact]
    public async Task Test_GetCustomersByIdName_WhenCalled_WithNoExistName_ReturnsNotFoundAsync()
    {
        // Arrange
        contextMock.Setup<DbSet<CustomerDB>>(x => x.Customers)
            .ReturnsDbSet(DataHelper.GetFakeCustomerList);

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = await customerController.GetAsync("Mory");

        // Assert
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task Test_GetCustomersByIdName_WhenCalled_ReturnsServerError()
    {
        // Arrange
        contextMock.Setup<DbSet<CustomerDB>?>(x => x.Customers)
            .Throws<InvalidOperationException>();

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = await customerController.GetAsync();

        //Assert
        var stc = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(500, stc.StatusCode);
    }
}
