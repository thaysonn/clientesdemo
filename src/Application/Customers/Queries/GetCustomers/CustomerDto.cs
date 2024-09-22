using Demo.Domain.Entities;

namespace Demo.Application.Customers.Queries.GetCustomers;
public class CustomerDto
{
    public int Id { get; init; } 

    public string? NomeEmpresa { get; init; } 

    public int PorteEmpresa { get; init; } 

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Customer, CustomerDto>().ForMember(d => d.PorteEmpresa,
                opt => opt.MapFrom(s => (int)s.PorteEmpresa));
        }
    }
}
