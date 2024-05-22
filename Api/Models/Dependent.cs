using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Dependent
{
    public int Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    
    [Required]
    public string? LastName { get; set; }
    
    [Required]
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }
    
    [Required]
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
