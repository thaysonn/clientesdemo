using Demo.Application.Common.Interfaces;
using Demo.Domain.Events;

namespace Demo.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
           .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.AddDomainEvent(new CustomerUpdatedEvent(entity));

        entity.NomeEmpresa = request.NomeEmpresa;
        entity.PorteEmpresa = request.PorteEmpresa;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
