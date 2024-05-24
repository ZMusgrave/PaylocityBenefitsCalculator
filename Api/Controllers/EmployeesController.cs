
using Api.Data;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    
    private readonly CompanyContext _context;

    public EmployeesController(CompanyContext context)
    {
        _context = context;

        _context.Database.EnsureCreated();
    }
    
    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        try
        {
            var employee = await _context.Employees
                .Include(e => e.Dependents)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return NotFound(new ApiResponse<GetDependentDto>
                {
                    Success = false,
                    Error = $"Dependent with ID {id} not found."
                });
            }
            
            var employeeDto = new GetEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Dependents = employee.Dependents.Select(d => new GetDependentDto
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    DateOfBirth = d.DateOfBirth,
                    Relationship = d.Relationship
                }).ToList()
            };
            
            var result = new ApiResponse<GetEmployeeDto>
            {
                Data = employeeDto,
                Success = true
            };

            return Ok(result);
            
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<GetDependentDto>
            {
                Success = false,
                Error = ex.Message
            });
        }
      
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        try
        {
            IQueryable<Employee> employeeData = _context.Employees.Include(e => e.Dependents);

            var employees = await employeeData.Select(e => new GetEmployeeDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Salary = e.Salary,
                DateOfBirth = e.DateOfBirth,
                Dependents = e.Dependents.Select(d => new GetDependentDto
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    DateOfBirth = d.DateOfBirth,
                    Relationship = d.Relationship
                }).ToList()
            }).ToListAsync();


            var result = new ApiResponse<List<GetEmployeeDto>>
            {
                Data = employees,
                Success = true
            };

            return result;
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<GetDependentDto>
            {
                Success = false,
                Error = ex.Message
            });
        }
    }
    
    [SwaggerOperation(Summary = "Create a new employee")]
    [HttpPost("")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Post(Employee employee)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<GetDependentDto>
                {
                    Success = false,
                    Error = $"{ModelState.IsValid}"
                });
            }

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var employeeDto = new GetEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                DateOfBirth = employee.DateOfBirth,
                Dependents = employee.Dependents.Select(d => new GetDependentDto
                {
                    Id = d.Id,
                    FirstName = d.FirstName,
                    LastName = d.LastName,
                    DateOfBirth = d.DateOfBirth,
                    Relationship = d.Relationship
                }).ToList()
            };
            
            var result = new ApiResponse<GetEmployeeDto>
            {
                Data = employeeDto,
                Success = true
            };

            return Ok(result);
            
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<GetDependentDto>
            {
                Success = false,
                Error = ex.Message
            });
        }
      
    }
}
