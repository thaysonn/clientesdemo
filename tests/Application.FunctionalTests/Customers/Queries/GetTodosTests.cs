using Demo.Application.Customers.Queries.GetCustomers;
using Demo.Domain.Entities; 

namespace Demo.Application.FunctionalTests.Customers.Queries;

using static Testing;

public class GetCustomersTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPortesEmpresa()
    {
        await RunAsDefaultUserAsync();

        var query = new GetCustomersQuery();

        var result = await SendAsync(query);

        result.PortesEmpresa.Should().NotBeEmpty();
    }

    [Test]
    public async Task ShouldReturnAllCustomers()
    {
        await RunAsDefaultUserAsync();

        await AddAsync(new Customer { NomeEmpresa = "Microsoft", PorteEmpresa = Domain.Enums.PorteEmpresa.Grande });

        var query = new GetCustomersQuery();

        var result = await SendAsync(query);

        result.Lists.Should().HaveCount(1);
        result.Lists.First().PorteEmpresa.Equals((int)Domain.Enums.PorteEmpresa.Grande);
    }

    [Test]
    public async Task ShouldDenyAnonymousUser()
    {
        var query = new GetCustomersQuery();

        var action = () => SendAsync(query);
        
        await action.Should().ThrowAsync<UnauthorizedAccessException>();
    }
}
