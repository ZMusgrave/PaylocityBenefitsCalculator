using Api.Data;
using Api.Dtos.Dependent;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly CompanyContext _context;

    public DependentsController(CompanyContext context)
    {
        _context = context;

        _context.Database.EnsureCreated();
    }
     
    
    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        try
        {
            var dependent = await _context.Dependents.FindAsync(id);

            if (dependent == null)
            {
                return NotFound(new ApiResponse<GetDependentDto>
                {
                    Success = false,
                    Error = $"Dependent with ID {id} not found."
                });
            }

            var dependentDto = new GetDependentDto
            {
                Id = dependent.Id,
                FirstName = dependent.FirstName,
                LastName = dependent.LastName,
                DateOfBirth = dependent.DateOfBirth,
                Relationship = dependent.Relationship
            };

            var result = new ApiResponse<GetDependentDto>
            {
                Data = dependentDto,
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

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        try
        {
            IQueryable<Dependent> dependentData = _context.Dependents;

            var dependents = await dependentData.Select(d => new GetDependentDto()
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                DateOfBirth = d.DateOfBirth,
                Relationship = d.Relationship
            }).ToListAsync();
           
            var result = new ApiResponse<List<GetDependentDto>>
            {
                Data = dependents,
                Success = true
            };

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<List<GetDependentDto>>
            {
                Success = false,
                Error = ex.Message
            });
        }

    }
}
