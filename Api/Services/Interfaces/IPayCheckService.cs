using Api.Dtos.Finance.Paycheck;
using Api.Models;

namespace Api.Services.Interfaces;

public interface IPaycheckService
{
        GetPaycheckDto CalculatePaycheck(Employee employee);
}