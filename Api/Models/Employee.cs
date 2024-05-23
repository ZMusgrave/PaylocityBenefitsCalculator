using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Employee
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [Required]
    public decimal Salary { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    
    [ValidateDependents]
    public ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();
  
}
