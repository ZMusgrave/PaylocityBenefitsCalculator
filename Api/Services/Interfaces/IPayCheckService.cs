using Api.Dtos.Finance.Paycheck;
using Api.Models;

namespace Api.Services.Interfaces;

public interface IPayCheckCalculator
{
        GetPaycheckDto CalculatePaycheck(Employee employee);
}