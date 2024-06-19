using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq.EntityFrameworkCore;
using CarShop.Tests.Helpers;

namespace CarShop.Tests.CustomersControllerTest.Get;

public class GetCustomers
{
    private readonly Mock<CarShopDataContext> contextMock;
    public GetCustomers() {
        contextMock = new Mock<CarShopDataContext>();
    }

    [Fact]
    public async Task Test_GetCustomers_WhenCalled_ReturnsCustomersListAsync()
    {
        // Arrange
        var expected = TestDataHelper.GetFakeCustomerList();
        contextMock.Setup<DbSet<CustomerDB>>(x => x.Customers)
            .ReturnsDbSet(TestDataHelper.GetFakeCustomerList());

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = await customerController.Get();

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var customers = Assert.IsType<CustomerDB[]>(okResult.Value);
        Assert.NotNull(customers);
        Assert.Equal(2, customers.Count());
        Assert.Equal<CustomerDB>(expected[0], customers.ElementAt(0));
        Assert.Equal<CustomerDB>(expected[1], customers.ElementAt(1));
    }

    [Fact]
    public async Task Test_GetCustomers_WhenCalled_ReturnsNotFound()
    {
        // Arrange
        contextMock.Setup<DbSet<CustomerDB>>(x => x.Customers)
            .ReturnsDbSet(TestDataHelper.GetFakeNullCustomerList());

        //Act
        CustomersController customerController = new(contextMock.Object);
        var customers = await customerController.Get();

        //Assert
        Assert.IsType<NotFoundResult>(customers.Result);
    }

    [Fact]
    public async Task Test_GetCustomers_WhenCalled_ReturnsServerError()
    {
        // Arrange
        contextMock.Setup<DbSet<CustomerDB>>(x => x.Customers)
            .Throws<InvalidOperationException>();

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = await customerController.Get();

        //Assert
        var stc = Assert.IsType<ObjectResult>(actionResult.Result);
        Assert.Equal(500, stc.StatusCode);
    }
}
