using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Dependent
{
    public int Id { get; set; }
    [Required]
    [StringLength(30)]
    public string? FirstName { get; set; }
    [Required]
    [StringLength(30)]
    public string? LastName { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    [Required]
    public Relationship Relationship { get; set; }
    [Required]
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }
}
