using System.Net;
using System.Threading.Tasks;
using Api.Dtos.Finance.Paycheck;
using Xunit;

namespace ApiTests.IntegrationTests;

public class PaymentIntegrationTests : IntegrationTest
{
    [Fact]
    public async Task GetPaycheck_ReturnsOkResult_WithValidEmployeeId()
    {
        var response = await HttpClient.GetAsync("/api/v1/paychecks/1");
        
        var paycheck = new GetPaycheckDto()
        {
            Id = "1",
            EmployeeId = 1,
            FirstName = "LeBron",
            LastName = "James",
            Gross = 2900.81m,
            BenefitsCost = 461.54m,
            Net = 2439.27m
        };
        
        
        await response.ShouldReturn(HttpStatusCode.OK, paycheck);
    }
}