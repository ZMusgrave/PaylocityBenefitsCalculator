
namespace Api.Dtos.Finance.Paycheck;

public class GetPaycheckDto
{
    public string? Id { get; set; }

    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public decimal Gross { get; set; }

    public decimal Net { get; set; }
    
    public decimal BenefitsCost { get; set; }
}