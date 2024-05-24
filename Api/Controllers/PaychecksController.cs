using Api.Data;
using Api.Dtos.Dependent;
using Api.Dtos.Finance.Paycheck;
using Api.Models;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PaychecksController : ControllerBase
{
    private readonly CompanyContext _context;
    private readonly IPaycheckService _paycheckService;

    public PaychecksController(CompanyContext context, IPaycheckService paycheckService)
    {
        _context = context;
        _context.Database.EnsureCreated();
        
        _paycheckService = paycheckService;
    }
    
    [SwaggerOperation(Summary = "Get paycheck by Employee Id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetPaycheckDto>>> Get(int id)
    {
        try
        {
            var employee = await _context.Employees
                .Include(e => e.Dependents)
                .FirstOrDefaultAsync(e => e.Id == id);
            
            if (employee == null)
            {
                return NotFound(new ApiResponse<GetPaycheckDto>
                {
                    Success = false,
                    Error = $"Employee with ID {id} not found."
                });
            }
           
            var paycheck = _paycheckService.CalculatePaycheck(employee);
            
            var result = new ApiResponse<GetPaycheckDto>
            {
                Data = paycheck,
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