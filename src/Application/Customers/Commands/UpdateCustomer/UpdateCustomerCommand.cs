using Demo.Domain.Enums;

namespace Demo.Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand : IRequest
{
    public int Id { get; init; }

    public string? NomeEmpresa { get; init; }

    public PorteEmpresa PorteEmpresa { get; init; }
}
