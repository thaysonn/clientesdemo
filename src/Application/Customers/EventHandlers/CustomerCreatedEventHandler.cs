using Demo.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Demo.Application.Customers.EventHandlers;

public class CustomerCreatedEventHandler : INotificationHandler<CustomerCreatedEvent>
{
    private readonly ILogger<CustomerCreatedEventHandler> _logger;

    public CustomerCreatedEventHandler(ILogger<CustomerCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Demo Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
