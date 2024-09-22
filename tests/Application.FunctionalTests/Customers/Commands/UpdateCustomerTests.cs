using Demo.Application.Common.Exceptions;
using Demo.Application.Customers.Commands.CreateCustomer;
using Demo.Application.Customers.Commands.UpdateCustomer;
using Demo.Domain.Entities;

namespace Demo.Application.FunctionalTests.Customers.Commands;

using static Testing;

public class UpdateCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidCustomerId()
    {
        var command = new UpdateCustomerCommand { Id = 99, NomeEmpresa = "Google", PorteEmpresa = Domain.Enums.PorteEmpresa.Grande };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldRequireUniqueNomeEmpresas()
    {
        var id = await SendAsync(new CreateCustomerCommand
        {
            NomeEmpresa = "Apple", 
            PorteEmpresa = Domain.Enums.PorteEmpresa.Grande
        });

        await SendAsync(new CreateCustomerCommand
        {
            NomeEmpresa = "Oracle",
            PorteEmpresa = Domain.Enums.PorteEmpresa.Grande
        });

        var command = new UpdateCustomerCommand
        {
            Id = id,
            NomeEmpresa = "Oracle"
        };

        (await FluentActions.Invoking(() =>
            SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("NomeEmpresa")))
                .And.Errors["NomeEmpresa"].Should().Contain("'Nome Empresa' deve ser único.");
    }

    [Test]
    public async Task ShouldUpdatedCustomer()
    {
        var userId = await RunAsDefaultUserAsync();

        var id = await SendAsync(new CreateCustomerCommand
        {
            NomeEmpresa = "Empresa X",
            PorteEmpresa = Domain.Enums.PorteEmpresa.Grande

        });

        var command = new UpdateCustomerCommand
        {
            Id = id,
            NomeEmpresa = "Updated Empresa X",
            PorteEmpresa = Domain.Enums.PorteEmpresa.Grande
        };

        await SendAsync(command);

        var c = await FindAsync<Customer>(id);

        c.Should().NotBeNull();
        c!.NomeEmpresa.Should().Be(command.NomeEmpresa);
        c.LastModifiedBy.Should().NotBeNull();
        c.LastModifiedBy.Should().Be(userId);
        c.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
