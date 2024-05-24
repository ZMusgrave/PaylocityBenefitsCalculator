using Api.Dtos.Finance.Paycheck;
using Api.Models;
using Api.Services.Interfaces;

namespace Api.Services;

public class PaycheckService : IPaycheckService
{
    private const int PaychecksPerYear = 26;
    private const decimal BaseBenefitsCostPerMonth = 1000;
    private const decimal DependentBenefitsCostPerMonth = 600;
    private const decimal HighEarnerSalaryThreshold = 80000;
    private const decimal HighEarnerBenefitsRate = 0.02m;
    private const decimal OlderDependentAgeCutoff = 50;
    private const decimal OlderDependentAdditionalCostPerMonth = 200;

    public GetPaycheckDto CalculatePaycheck(Employee employee)
    {
        decimal annualBaseBenefitsCost = BaseBenefitsCostPerMonth * 12;
        decimal annualDependentBenefitsCost = CalculateAnnualDependentBenefitsCost(employee.Dependents.ToList());
        decimal annualHighEarnerBenefitsCost = CalculateAnnualHighEarnerBenefitsCost(employee.Salary);
        decimal annualOlderDependentBenefitsCost = CalculateAnnualOlderDependentBenefitsCost(employee.Dependents.ToList());

        decimal totalAnnualBenefitsCost = annualBaseBenefitsCost + annualDependentBenefitsCost +
                                           annualHighEarnerBenefitsCost + annualOlderDependentBenefitsCost;

        decimal perPaycheckBenefitsCost =
            Math.Round(totalAnnualBenefitsCost / PaychecksPerYear, 2, MidpointRounding.ToEven);
        decimal perPayCheckGross = Math.Round(employee.Salary / PaychecksPerYear, 2, MidpointRounding.ToEven);
        decimal net = Math.Round(perPayCheckGross - perPaycheckBenefitsCost, 2, MidpointRounding.ToEven);

        var payCheck = new GetPaycheckDto()
        {
            Id = "1",
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            EmployeeId = employee.Id,
            Gross = perPayCheckGross,
            BenefitsCost = perPaycheckBenefitsCost,
            Net = net
        };
            
        return payCheck;
    }

    private decimal CalculateAnnualDependentBenefitsCost(List<Dependent> dependents)
    {
        int dependentCount = dependents.Count;
        return dependentCount * DependentBenefitsCostPerMonth * 12;
    }

    private decimal CalculateAnnualHighEarnerBenefitsCost(decimal salary)
    {
        if (salary > HighEarnerSalaryThreshold)
        {
            return salary * HighEarnerBenefitsRate;
        }
        return 0;
    }

    private decimal CalculateAnnualOlderDependentBenefitsCost(List<Dependent> dependents)
    {
        int olderDependentCount = dependents.Count(d => CalculateAge(d.DateOfBirth) > OlderDependentAgeCutoff);
        return olderDependentCount * OlderDependentAdditionalCostPerMonth * 12;
    }

    private int CalculateAge(DateTime dateOfBirth)
    {
        int age = 0;
        age = DateTime.Now.Year - dateOfBirth.Year;
        if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            age = age - 1;

        return age;
    }
}
