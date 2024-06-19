using Microsoft.EntityFrameworkCore;
using Moq.EntityFrameworkCore;
using CarShop.Tests.Helpers;

namespace CarShop.Tests;

public class CustomerControllerUnitTest
{
    [Fact]
    public async Task Test_GetCustomers_WhenCalled_ReturnsCustomersListAsync()
    {
        // Arrange
        var expected = TestDataHelper.GetFakeCustomerList();
        var contextMock = new Mock<CarShopDataContext>();
        contextMock.Setup<DbSet<CustomerDB>>(x => x.Customers)
            .ReturnsDbSet(TestDataHelper.GetFakeCustomerList());

        //Act
        CustomersController customerController = new(contextMock.Object);
        var customers = (await customerController.Get()).Value;

        //Assert
        Assert.NotNull(customers);
        Assert.Equal(2, customers.Count());
        Assert.Equal<CustomerDB>(expected[0], customers.ElementAt(0));
        Assert.Equal<CustomerDB>(expected[1], customers.ElementAt(1));
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
}
