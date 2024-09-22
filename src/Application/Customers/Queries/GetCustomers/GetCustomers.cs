using Demo.Application.Common.Interfaces;
using Demo.Application.Common.Models;
using Demo.Application.Common.Security;
using Demo.Domain.Enums;

namespace Demo.Application.Customers.Queries.GetCustomers;

[Authorize]
public record GetCustomersQuery : IRequest<CustomerVm>;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, CustomerVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerVm> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return new CustomerVm
        {
            PortesEmpresa = Enum.GetValues(typeof(PorteEmpresa))
                .Cast<PorteEmpresa>()
                .Select(p => new LookupDto { Id = (int)p, Title = p.ToString() })
                .ToList(),

            Lists = await _context.Customers
                .AsNoTracking()
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.NomeEmpresa)
                .ToListAsync(cancellationToken)
        };
    }
}
