# Employee Paycheck Calculation API

## Project Overview
This project is an API for managing employees and their dependents, and for calculating their monthly paycheck. The API supports basic GET operations for employees and dependents, a Post operation for creating new employees with dependents and a GET operation that calculates the paycheck based on various rules.

This project makes a swagger endpoint available with the above methods listed for use. 

### Features

Create, read, employees and their dependents based on restrictions.
Calculate the monthly paycheck for an employee based on:
A base cost for employees.
Additional costs for dependents.
Extra charges for high salary employees.
Additional charges for dependents over a certain age.

### Technologies Used

**ASP.NET Core 6.0**: Web framework for building the API.  
**Entity Framework Core**: ORM for database interactions.  
**EF In Memory DB**: Database for storing employee and dependent data.  
**xUnit**: Testing framework for unit and integration tests.  
**Swagger**: API documentation, design and testing for endpoints.  

## Getting Started

### Prerequisites

.NET 6.0 SDK 
A code editor like Visual Studio || Rider || Visual Studio Code

### Installation

Clone the repository:

```bash
git clone https://github.com/ZMusgrave/PaylocityBenefitsCalculator.git
cd employee-paycheck-api
```

### Run the application

```console
dotnet build
dotnet run
```

These commands will install any needed dependencies, build the project, and run
the project respectively.

#### You may need to provide a dev cert to the application, in that case run the following.

```console
dotnet dev-certs https --trust
```

### Project Structure

```
PaylocityBenefitsCalculator
API
│
├── Controllers
│   └── EmployeesController.cs
|   └── DependentsController.cs
|   └── PaychecksController.cs
│
├── Models
│   └── Employee.cs
│   └── Dependent.cs
│   └── Relationship.cs
|   └── Paycheck.cs
│   └── ApiResponse.cs
│   └── ValidateDependents.cs
│   └── ModelBuilderExtension.cs
│
├── DTOs
│   └── GetDependentDto.cs
│   └── GetEmployeeDto.cs
│   └── GetPaycheckDto.cs
│
├── Services
│   └── Interfaces
│       └── IPaycheckService.cs
│   └── PaycheckService.cs
│
├── Data
│   └── CompanyContext.cs
│
│
├── Program.cs
│
│
ApiTests
├── IntegrationTests
|   └── EmployeeIntegrationTests.cs
|   └── DependentIntegrationTests.cs
|   └── PaymentIntegrationTests.cs
├── UnitTests
|   └── PaycheckCalculatorTest.cs
├── IntegrationTest.cs
├── ShouldExtension.cs
```

### Testing

To Test the application - the api needs to be running so the endpoints are active and can be tested for functionality.

```console
dotnet build
dotnet run
```

Once the app is running, use the built in fixtures for testing via the ApiTests project. 

The tests check for functionality of endpoints and unit tests for individual method functionality where necessary. 




