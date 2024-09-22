using Demo.Domain.Enums;

namespace Demo.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand : IRequest<int>
{
    public string? NomeEmpresa { get; init; }
    public PorteEmpresa PorteEmpresa { get; init; }
}
