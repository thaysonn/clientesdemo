using Demo.Application.Common.Interfaces;
using Demo.Domain.Events;

namespace Demo.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.Customer();

        entity.NomeEmpresa = request.NomeEmpresa;
        entity.PorteEmpresa = request.PorteEmpresa;

        entity.AddDomainEvent(new CustomerCreatedEvent(entity));

        _context.Customers.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
