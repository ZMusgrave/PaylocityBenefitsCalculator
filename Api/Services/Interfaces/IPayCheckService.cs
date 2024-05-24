using Api.Models;

namespace Api.Services.Interfaces;

public interface IPayCheckCalculator
{
        Paycheck CalculatePaycheck(Employee employee);
}