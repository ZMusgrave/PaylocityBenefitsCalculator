using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class Paycheck
{
    [Required] public string Id { get; set; } = string.Empty;
    [Required]
    public int EmployeeId { get; set; }
    [Required]
    public decimal Gross { get; set; }
    [Required]
    public decimal BenefitsCost { get; set; }
    [Required]
    public decimal Net { get; set; }
    
}