using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Moq.EntityFrameworkCore;
using CarShop.Tests.Helpers;

namespace CarShop.Tests.CustomersControllerTest.Get;

public class GetCustomerById
{
    private readonly Mock<CarShopDataContext> contextMock;
    private TestDataHelper DataHelper;
    public GetCustomerById() {
        contextMock = new Mock<CarShopDataContext>();
        DataHelper = new TestDataHelper();
    }


    [Fact]
    public async Task Test_GetCustomerById_WhenCalled_ReturnsACustomerOkAsync()
    {
        // Arrange
        var expected = DataHelper.GetFakeCustomerList[0];
        contextMock.Setup<DbSet<CustomerDB>>(x => x.Customers)
            .ReturnsDbSet(DataHelper.GetFakeCustomerList);

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = await customerController.GetAsync(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var customer = Assert.IsType<CustomerDB>(okResult.Value);
        Assert.NotNull(customer);
        Assert.Equal(expected, customer);
    }

    [Fact]
    public async Task Test_GetCustomerById_WhenCalled_WithIdOutOfIndex_ReturnsNotFoundAsync()
    {
        // Arrange
        contextMock.Setup<DbSet<CustomerDB>>(x => x.Customers)
            .ReturnsDbSet(DataHelper.GetFakeCustomerList);

        //Act
        CustomersController customerController = new(contextMock.Object);
        var actionResult = await customerController.GetAsync(99);

        // Assert
        Assert.IsType<NotFoundResult>(actionResult.Result);
    }

    [Fact]
    public async Task Test_GetCustomerById_WhenCalled_ReturnsServerErrorAsync()
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
