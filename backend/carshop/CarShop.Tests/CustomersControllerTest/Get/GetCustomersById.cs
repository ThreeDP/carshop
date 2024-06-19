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

    // [Fact]
    // public async Task GetEmployeeById_WhenCalled_ReturnsEmployeeAsync()
    // {
    //     // Arrange            
    //     var employeeContextMock = new Mock<EmployeeDBContext>();
    //     employeeContextMock.Setup(x => x.Employees.FindAsync(1).Result)
    //         .Returns(TestDataHelper.GetFakeEmployeeList().Find(e => e.Id == 1) ?? new Employee());

    //     //Act
    //     EmployeesController employeesController = new(employeeContextMock.Object);
    //     var employee = (await employeesController.GetEmployeeById(1)).Value;

    //     //Assert
    //     Assert.NotNull(employee);
    //     Assert.Equal(1, employee.Id);
    // }

    [Fact]
    public async Task Test_GetCustomersById_WhenCalled_ReturnsACustomer()
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

    // [Fact]
    // public async Task Test_GetCustomers_WhenCalled_ReturnsServerError()
    // {
    //     // Arrange
    //     contextMock.Setup<DbSet<CustomerDB>>(x => x.Customers)
    //         .Throws<InvalidOperationException>();

    //     //Act
    //     CustomersController customerController = new(contextMock.Object);
    //     var customers = await customerController.Get();

    //     //Assert
    //     var objectResult = customers.Result as ObjectResult;
    //     Assert.Equal(500, objectResult.StatusCode);
    // }


}
