using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq.EntityFrameworkCore;
using CarShop.Tests.Helpers;

namespace CarShop.Tests.CustomersControllerTest.Get;

public class GetCustomers
{
    private readonly Mock<CarShopDataContext> contextMock;
    private TestDataHelper DataHelper;
    public GetCustomers() {
        contextMock = new Mock<CarShopDataContext>();
        DataHelper = new TestDataHelper();
    }

    [Fact]
    public async Task Test_GetCustomers_WhenCalled_ReturnsCustomersOkListAsync()
    {
        // Arrange
        var expected = DataHelper.GetFakeCustomerList;
        contextMock.Setup<DbSet<CustomerDB>?>(x => x.Customers)
            .ReturnsDbSet(DataHelper.GetFakeCustomerList);

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = await customerController.GetAsync();

        //Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var customers = Assert.IsType<CustomerDB[]>(okResult.Value);
        Assert.NotNull(customers);
        Assert.Equal(3, customers.Count());
        Assert.Equal<CustomerDB>(expected[0], customers.ElementAt(0));
        Assert.Equal<CustomerDB>(expected[1], customers.ElementAt(1));
        Assert.Equal<CustomerDB>(expected[2], customers.ElementAt(2));
    }

    // [Fact]
    // public async Task Test_GetCustomers_WhenCalled_ReturnsNotFound()
    // {
    //     // Arrange
    //     var expected = DataHelper.GetFakeCustomerList;
    //     contextMock.Setup<DbSet<CustomerDB>?>(x => x.Customers)
    //         .ReturnsDbSet(null);

    //     //Act
    //     CustomersController customerController = new(contextMock.Object);
    //     var actionResult = await customerController.Get();

    //     //Assert
    //     var NotFoundResult = Assert.IsType<NotFoundResult>(actionResult.Result);
    //     Assert.Null(NotFoundResult);
    // }

    [Fact]
    public async Task Test_GetCustomers_WhenCalled_ReturnsServerErrorAsync()
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
