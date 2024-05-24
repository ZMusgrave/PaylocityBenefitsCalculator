using Api.Models;

namespace Api.Services.Interfaces;

public interface IPayCheckCalculator
{
        decimal CalculatePaycheck(Employee employee);
}