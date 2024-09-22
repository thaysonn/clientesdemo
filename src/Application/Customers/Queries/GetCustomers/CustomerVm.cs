using Demo.Application.Common.Models;

namespace Demo.Application.Customers.Queries.GetCustomers;
public class CustomerVm
{
    public IReadOnlyCollection<LookupDto> PortesEmpresa { get; init; } = Array.Empty<LookupDto>();

    public IReadOnlyCollection<CustomerDto> Lists { get; init; } = Array.Empty<CustomerDto>();
}
