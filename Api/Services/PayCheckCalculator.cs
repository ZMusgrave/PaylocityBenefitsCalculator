using Api.Models;
using Api.Services.Interfaces;

namespace Api.Services;

public class PayCheckService : IPayCheckCalculator
{
    private const int PaychecksPerYear = 26;
    private const decimal BaseBenefitsCostPerMonth = 1000;
    private const decimal DependentBenefitsCostPerMonth = 600;
    private const decimal HighEarnerSalaryThreshold = 80000;
    private const decimal HighEarnerBenefitsRate = 0.02m;
    private const decimal OlderDependentAgeCutoff = 50;
    private const decimal OlderDependentAdditionalCostPerMonth = 200;

    public decimal CalculatePaycheck(Employee employee)
    {
        decimal annualBaseBenefitsCost = BaseBenefitsCostPerMonth * 12;
        decimal annualDependentBenefitsCost = CalculateDependentBenefitsCost(employee.Dependents.ToList());
        decimal annualHighEarnerBenefitsCost = CalculateHighEarnerBenefitsCost(employee.Salary);
        decimal annualOlderDependentBenefitsCost = CalculateOlderDependentBenefitsCost(employee.Dependents.ToList());

        decimal totalAnnualBenefitsCost = annualBaseBenefitsCost + annualDependentBenefitsCost +
                                           annualHighEarnerBenefitsCost + annualOlderDependentBenefitsCost;

        decimal perPaycheckBenefitsCost = totalAnnualBenefitsCost / PaychecksPerYear;
        decimal perPaycheckSalary = employee.Salary / PaychecksPerYear;

        decimal paycheckAmount = perPaycheckSalary - perPaycheckBenefitsCost;

        return paycheckAmount;
    }

    private decimal CalculateDependentBenefitsCost(List<Dependent> dependents)
    {
        int dependentCount = dependents.Count;
        return dependentCount * DependentBenefitsCostPerMonth * 12;
    }

    private decimal CalculateHighEarnerBenefitsCost(decimal salary)
    {
        if (salary > HighEarnerSalaryThreshold)
        {
            return salary * HighEarnerBenefitsRate;
        }
        return 0;
    }

    private decimal CalculateOlderDependentBenefitsCost(List<Dependent> dependents)
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
