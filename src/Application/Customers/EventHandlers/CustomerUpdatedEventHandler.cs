using Demo.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Demo.Application.Customers.EventHandlers;

public class CustomerUpdatedEventHandler : INotificationHandler<CustomerUpdatedEvent>
{
    private readonly ILogger<CustomerUpdatedEventHandler> _logger;

    public CustomerUpdatedEventHandler(ILogger<CustomerUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Demo Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
