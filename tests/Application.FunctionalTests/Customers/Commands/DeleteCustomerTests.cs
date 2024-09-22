using Demo.Application.Customers.Commands.CreateCustomer;
using Demo.Application.Customers.Commands.DeleteCustomer;
using Demo.Domain.Entities;

namespace Demo.Application.FunctionalTests.Customers.Commands;

using static Testing;

public class DeleteCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidCustomerId()
    {
        var command = new DeleteCustomerCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteCustomer()
    {
        var id = await SendAsync(new CreateCustomerCommand
        {
            NomeEmpresa = "Amazon",
            PorteEmpresa = Domain.Enums.PorteEmpresa.Media
        });

        await SendAsync(new DeleteCustomerCommand(id));

        var list = await FindAsync<Customer>(id);

        list.Should().BeNull();
    }
}
