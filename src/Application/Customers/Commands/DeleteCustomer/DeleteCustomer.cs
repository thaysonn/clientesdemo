using Demo.Application.Common.Interfaces;
using Demo.Domain.Events;

namespace Demo.Application.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.AddDomainEvent(new CustomerDeletedEvent(entity));

        _context.Customers.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
