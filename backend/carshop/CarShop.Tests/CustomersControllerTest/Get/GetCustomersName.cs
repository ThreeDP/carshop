using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq.EntityFrameworkCore;
using CarShop.Tests.Helpers;

namespace CarShop.Tests.CustomersControllerTest.Get;

public class GetCustomerName
{
    private readonly Mock<CarShopDataContext> contextMock;
    public GetCustomerName() {
        contextMock = new Mock<CarShopDataContext>();
    }

    [Fact]
    public async Task Test_GetCustomerName_WhenCalled_ReturnsCustomersListAsync()
    {
        // Arrange
        var expected = TestDataHelper.GetFakeCustomerList();
        contextMock.Setup<DbSet<CustomerDB>?>(x => x.Customers)
            .ReturnsDbSet(TestDataHelper.GetFakeCustomerList());

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = customerController.Get("John Doe");

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var customer = Assert.IsType<List<CustomerDB>>(okResult.Value);
        Assert.NotNull(customer);
        Assert.Equal(expected[0], customer.ElementAt(0));
    }

        [Fact]
    public async Task Test_GetCustomerName_WhenCalled_WithNoExistName_ReturnsNotFound()
    {
        // Arrange
        contextMock.Setup<DbSet<CustomerDB>>(x => x.Customers)
            .ReturnsDbSet(TestDataHelper.GetFakeCustomerList());

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = customerController.Get("Mory");

        // Assert
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task Test_GetCustomerName_WhenCalled_ReturnsServerError()
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
