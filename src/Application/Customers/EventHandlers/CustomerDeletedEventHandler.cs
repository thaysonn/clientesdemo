using Demo.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Demo.Application.Customers.EventHandlers;

public class CustomerDeletedEventHandler : INotificationHandler<CustomerDeletedEvent>
{
    private readonly ILogger<CustomerDeletedEventHandler> _logger;

    public CustomerDeletedEventHandler(ILogger<CustomerDeletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Demo Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
