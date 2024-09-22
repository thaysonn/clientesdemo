namespace Demo.Domain.Events;

public class CustomerCreatedEvent : BaseEvent
{
    public CustomerCreatedEvent(Customer item)
    {
        Item = item;
    }

    public Customer Item { get; }
}
