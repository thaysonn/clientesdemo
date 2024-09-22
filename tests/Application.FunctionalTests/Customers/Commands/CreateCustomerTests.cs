using Demo.Application.Common.Exceptions;
using Demo.Application.Customers.Commands.CreateCustomer;
using Demo.Domain.Entities;

namespace Demo.Application.FunctionalTests.Customers.Commands;

using static Testing;

public class CreateCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateCustomerCommand();
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireUniqueNomeEmpresa()
    {
        await SendAsync(new CreateCustomerCommand
        {
            NomeEmpresa = "Suzuki",
            PorteEmpresa = Domain.Enums.PorteEmpresa.Pequena
        });

        var command = new CreateCustomerCommand
        {
            NomeEmpresa = "Suzuki",
            PorteEmpresa = Domain.Enums.PorteEmpresa.Pequena
        };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCustomer()
    {
        var userId = await RunAsDefaultUserAsync();

        var command = new CreateCustomerCommand
        {
            NomeEmpresa = "Ambev",
            PorteEmpresa = Domain.Enums.PorteEmpresa.Media
        };

        var id = await SendAsync(command);
        var customer = await FindAsync<Customer>(id);

        customer.Should().NotBeNull();
        customer!.NomeEmpresa.Should().Be(command.NomeEmpresa);
        customer.CreatedBy.Should().Be(userId);
        customer.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
