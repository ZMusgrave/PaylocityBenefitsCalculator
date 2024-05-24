# Employee Paycheck Calculation API

## Project Overview
This project is an API for managing employees and their dependents, and for calculating their monthly paycheck. The API supports basic CRUD operations for employees and dependents, and calculates the paycheck based on various rules.

### Features

Create, read, update, and delete employees and their dependents.
Calculate the monthly paycheck for an employee based on:
A base cost for employees.
Additional costs for dependents.
Extra charges for high salary employees.
Additional charges for dependents over a certain age.

### Technologies Used

**ASP.NET Core 6.0**: Web framework for building the API.
**Entity Framework Core**: ORM for database interactions.
**SQLite**: Database for storing employee and dependent data.
**xUnit**: Testing framework for unit and integration tests.
**FluentAssertions**: Library for more readable assertions in tests.

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

### Project Structure

```
PaylocityBenefitsCalculator
│
├── Controllers
│   └── EmployeesController.cs
│
├── Models
│   └── Employee.cs
│   └── Dependent.cs
│   └── Relationship.cs
│
├── DTOs
│   └── CreateEmployeeDto.cs
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
│   └── ApplicationDbContext.cs
│
├── Migrations
│
├── Program.cs
│
├── Startup.cs
│
└── Tests
    └── EmployeeApiTests.cs
```
