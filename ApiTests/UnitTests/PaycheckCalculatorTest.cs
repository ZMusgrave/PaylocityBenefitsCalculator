using System;
using System.Collections.Generic;
using Api.Models;
using Api.Services;
using Xunit;

namespace ApiTests.UnitTests;

public class PaycheckCalculatorTest
{
    [Fact]
    public void CalculatePaycheck_ReturnsCorrectPaycheckDto()
    {
        var employee = new Employee
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Salary = 60000,
            Dependents = new List<Dependent>
            {
                new() { FirstName = "Alice", LastName = "Gordon",DateOfBirth = new DateTime(2000, 1, 1) },
                new() { FirstName = "Bob", LastName = "Barron", DateOfBirth = new DateTime(1960, 1, 1) }
            }
        };
        var paycheckCalculator = new PaycheckService();

   
        var paycheck = paycheckCalculator.CalculatePaycheck(employee);

        Assert.NotNull(paycheck.Id);
        Assert.Equal(employee.FirstName, paycheck.FirstName);
        Assert.Equal(employee.LastName, paycheck.LastName);
        Assert.Equal(employee.Id, paycheck.EmployeeId);
        Assert.Equal(2307.69m, paycheck.Gross, 2);
        Assert.Equal(1107.69m, paycheck.BenefitsCost, 2);
        Assert.Equal(1200m, paycheck.Net, 2);
    }
}